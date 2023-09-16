using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    private const string NEW_NAME_INPUT_TEXT = "It looks like its the first time you play this game. Please input your player name in the box below.";
    private const string CHANGE_NAME_INPUT_TEXT = "Please input your new player name in the box below.";
    private const string NO_CONNECTION_NEW_PLAYER_TEXT = "It seems you are not connected to the internet. Internet is required to create a new player name.";
    private const string NO_CONNECTION_EXISTING_PLAYER_TEXT = "It seems you are not connected to the internet. You can still play the game, but Internet is required to upload your highscore";

    [SerializeField] private Button playButton;
    [SerializeField] private Button scoreboardButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject playerNameDisplay;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private GameObject panelNameInput;
    [SerializeField] private TMP_Text panelNameInputText;
    [SerializeField] private GameObject successNameChangeText;
    [SerializeField] private GameObject loadingObject;
    [SerializeField] private GameObject loadingSuccessObject;
    [SerializeField] private JSONSaving saveManager;
    [SerializeField] private CheckInternetConnection checkInternetConnection;
    [SerializeField] private GameObject internetConnectionPanel;
    [SerializeField] private TMP_Text internetConnectionText;
    [SerializeField] private GameObject changePlayerNameButton;

    private PlayerData playerData;

    private bool requestNameChange;
    private bool internetAvailable;
    private string playerName;
    private MenuState menuState;

    private enum MenuState
    {
        NewPlayer,
        ExistingPlayer
    }

    // Start is called before the first frame update
    void Start()
    {
        loadingSuccessObject.SetActive(false);
        ShowLoadingText();
        requestNameChange = false;
        playerName = GetPlayerName();
        internetConnectionPanel.SetActive(false);
        checkInternetConnection.TryLbConnection();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerName != "")
        {
            menuState = MenuState.ExistingPlayer;
        } else 
        {
            menuState = MenuState.NewPlayer;
        }
        SetupMainMenu(menuState);
    }

    private void SetupMainMenu(MenuState state)
    {
        if (state == MenuState.ExistingPlayer)
        {
            ShowPlayerNameDisplay();
            ShowMainPanel();
            playerNameText.SetText("Player: " + playerName);
            if (!requestNameChange || !internetAvailable)
            {
                HideNameInputPanel();
            }
            if (!internetAvailable)
            {
                ActivateChangePlayerNameButton(false);
                MakeInteractableScoreboardButton(false);
            }
        } else if (state == MenuState.NewPlayer)
        {
            if (internetAvailable)
            {
                ShowNewNamePanel();
            }
            else
            {
                HideNameInputPanel();
            }
            HideMainPanel();
            HidePlayerNameDisplay();
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadScoreboardScene()
    {
        SceneManager.LoadScene("Scoreboard");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private string GetPlayerName()
    {
        playerData = saveManager.LoadData();
        return playerData.name;
    }

    public void ShowChangeTrigger()
    {
        playerName = GetPlayerName();
    }

    public void ShowNewNamePanel()
    {
        ShowNameInputPanel();
        panelNameInputText.SetText(NEW_NAME_INPUT_TEXT);
    }

    public void OpenChangeNamePanel()
    {
        successNameChangeText.SetActive(false);
        requestNameChange = !requestNameChange;
        ShowNameInputPanel();
        panelNameInputText.SetText(CHANGE_NAME_INPUT_TEXT);
    }

    public void InternetConnectionPanel(bool status)
    {
        HideLoadingText();
        if (status)
        {
            StartCoroutine(PopUpOnLoadSuccess());
        }
        internetConnectionPanel.SetActive(!status);
        if (menuState == MenuState.NewPlayer)
        {
            internetConnectionText.SetText(NO_CONNECTION_NEW_PLAYER_TEXT);
        } else if(menuState == MenuState.ExistingPlayer)
        {
            internetConnectionText.SetText(NO_CONNECTION_EXISTING_PLAYER_TEXT);
        }
        ActivateChangePlayerNameButton(status);
        MakeInteractableScoreboardButton(status);
        internetAvailable = status;
    }

    IEnumerator PopUpOnLoadSuccess()
    {
        loadingSuccessObject.SetActive(true);
        yield return new WaitForSeconds(1);
        loadingSuccessObject.SetActive(false);
    }

    void MakeInteractableScoreboardButton(bool status)
    {
        scoreboardButton.interactable = status;
    }

    void ActivateChangePlayerNameButton(bool status)
    {
        changePlayerNameButton.SetActive(status);
    }
    
    void ShowMainPanel()
    {
        panelMainMenu.SetActive(true);
    }

    void HideMainPanel()
    {
        panelMainMenu.SetActive(false);
    }

    void ShowPlayerNameDisplay()
    {
        playerNameDisplay.SetActive(true);
    }

    void HidePlayerNameDisplay()
    {
        playerNameDisplay.SetActive(false);
    }

    void ShowNameInputPanel()
    {
        panelNameInput.SetActive(true);
    }

    void HideNameInputPanel()
    {
        panelNameInput.SetActive(false);
    }

    void ShowLoadingText()
    {
        loadingObject.SetActive(true);
    }

    void HideLoadingText()
    {
        loadingObject.SetActive(false);
    }
}
