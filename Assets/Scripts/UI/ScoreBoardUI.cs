using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreBoardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        highScoreDisplay.SetText(GetHighScore().ToString());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private int GetHighScore()
    {
        return SaveManager.LoadInt("HighScore");
    }
}
