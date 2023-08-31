using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgainMenuController : MonoBehaviour
{
    public Button replayButton;
    public GameObject replayPanel;
    public GameObject inGameUI;
    [SerializeField] private GameObject scoreManagerObject;
    [SerializeField] private GameObject successfulText;
    [SerializeField] private GameObject errorText;
    [SerializeField] private GameObject highScoreText;
    private PlayerController playerController;
    private ScoreManager scoreManager;


    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Button replayButtonComponent = replayButton.GetComponent<Button>(); 
        replayButtonComponent.onClick.AddListener(ResetScene);
        replayPanel.SetActive(false);
        inGameUI.SetActive(true);
        successfulText.SetActive(false);
        errorText.SetActive(false);
        highScoreText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.IsDead())
        {
            Invoke("ShowReplayMenu", 2);
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    void ShowReplayMenu()
    {
        replayPanel.SetActive(true);
        inGameUI.SetActive(false);
        Time.timeScale = 0;
    }

    
    public void IsSubmitSuccessful(bool isSuccessful)
    {
        if (isSuccessful)
        {
            successfulText.SetActive(true);
        } else
        {
            errorText.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void ShowNewHighScoreText()
    {
        highScoreText.SetActive(true);
    }
}
