using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject playerNameDisplay;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private GameObject panelNameInput;
    [SerializeField] private TMP_Text panelNameInputText;
    [SerializeField] private GameObject successNameChangeText;

    private bool RequestNameChange;



    // Start is called before the first frame update
    void Start()
    {

        RequestNameChange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetPlayerName() != "")
        {
            ShowPlayerNameDisplay();
            ShowMainPanel();
            playerNameText.SetText("Player: " + GetPlayerName());
            if (!RequestNameChange)
            {
                HideNameInputPanel();
            }
        } else
        {
            HidePlayerNameDisplay();
            HideMainPanel();
            OpenNewNamePanel();
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
        return SaveManager.LoadString("PlayerName");
    }

    public void OpenNewNamePanel()
    {
        ShowNameInputPanel();
        panelNameInputText.SetText("It looks like its the first time you play this game. Please input your player name in the box below.");
    }

    public void OpenChangeNamePanel()
    {
        successNameChangeText.SetActive(false);
        RequestNameChange = !RequestNameChange;
        ShowNameInputPanel();
        panelNameInputText.SetText("Please input your new player name in the box below.");
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
}
