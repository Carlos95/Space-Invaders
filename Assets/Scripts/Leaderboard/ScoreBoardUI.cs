using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ScoreBoardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreDisplay;
    [SerializeField] private JSONSaving saveManager;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Leaderboard lb;
    [SerializeField] private GameObject focusScoreButton;

    // Start is called before the first frame update
    void Start()
    {
        highScoreDisplay.SetText(saveManager.LoadData().score.ToString());
    }

    private void Update()
    {
        SetFocusScoreButtonActive();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void SnapToTarget()
    {
        if (lb.playerEntryRect != null)
        {
            StartCoroutine(StopInertia());
            Vector2 targetPosition = new Vector2(0, -lb.playerEntryRect.anchoredPosition.y);
            scrollRect.content.anchoredPosition = targetPosition;
        }
    }

    private void SetFocusScoreButtonActive()
    {
        if (lb.playerEntryRect == null)
        {
            focusScoreButton.SetActive(false);
        }
        else
        {
            focusScoreButton.SetActive(true);
        }
    }

    IEnumerator StopInertia()
    {
        scrollRect.inertia = false;
        yield return new WaitForSeconds(0.1f);
        scrollRect.inertia = true;
    }
}
