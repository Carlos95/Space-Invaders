using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text highScoreDisplay;
    private TMP_Text additionScoreDisplay;
    [SerializeField] private GameObject highScoreObject;
    [SerializeField] private GameObject additionScoreObject;
    private int score;
    private PlayerController playerController;

    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        additionScoreDisplay = additionScoreObject.GetComponent<TMP_Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highScoreObject.SetActive(false);
        additionScoreObject.SetActive(false);

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
        //Debug.Log("Score Added: " + scoreAddition);
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


}
