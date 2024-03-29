using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private int score;
    public TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text highScoreDisplay;
    private TMP_Text additionScoreDisplay;
    [SerializeField] private GameObject highScoreObject;
    [SerializeField] private GameObject additionScoreObject;
    [SerializeField] private GameObject canvas;
    private PlayerController playerController;
    private PlayAgainMenuController playAgainMenuController;
    [SerializeField] private JSONSaving saveManager;
    private PlayerData playerData;

    private bool HasPerformedSubmit;
    public UnityEvent<string, int> submitScoreEvent;
    public UnityEvent<string> getLeaderboardEvent;

    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        additionScoreDisplay = additionScoreObject.GetComponent<TMP_Text>();
        playAgainMenuController = canvas.GetComponent<PlayAgainMenuController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highScoreObject.SetActive(false);
        additionScoreObject.SetActive(false);
        HasPerformedSubmit = false;
        playerData = saveManager.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        SetScore();
        ShowScoreOnPlayerDeath();
    }

    void FixedUpdate()
    {
        DistanceScore();
    }

    public void AddScore(int scoreAddition)
    {
        score += scoreAddition;
        if (scoreAddition > 1)
        {
            ShowAdditionScore(scoreAddition);
        }
    }

    void SetScore()
    {
        scoreDisplay.SetText(score.ToString());
    }

    void ShowScoreOnPlayerDeath()
    {
        if (playerController.IsDead())
        {
            highScoreDisplay.SetText(score.ToString());
            highScoreObject.SetActive(true);
            if (!HasPerformedSubmit)
            {
                if (IsNewHighscore())
                {
                    SubmitScore();
                    playAgainMenuController.ShowNewHighScoreText();
                    playAgainMenuController.ShowUploadingText();
                }
                else
                {
                    GetLeaderboard();
                }
                HasPerformedSubmit = true;
            }
        }
    }
    
    // If player is not dead, it will add 1 to score
    void DistanceScore()
    {
        if (!playerController.IsDead())
        {
            AddScore(1);
        }
    }

    public void ShowAdditionScore(int scoreToAdd)
    {
        StartCoroutine(PopUpAnimation(scoreToAdd));
    }
    
    
    IEnumerator PopUpAnimation(int scoreToAdd)
    {
        additionScoreObject.SetActive(true);
        additionScoreDisplay.SetText("+" + scoreToAdd.ToString());
        yield return new WaitForSeconds(2);
        additionScoreObject.SetActive(false);
    }

    public void SubmitScore()
    {
        submitScoreEvent.Invoke(GetPlayerName(), score);
    }


    public void GetLeaderboard()
    {
        getLeaderboardEvent.Invoke(GetPlayerName());
    }

    private string GetPlayerName()
    {
        return playerData.name;
    }

    private int GetHighScore()
    {
        return playerData.score;
    }

    public bool IsNewHighscore() 
    {
        return GetHighScore() < score;
    }
}
