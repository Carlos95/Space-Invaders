using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreBoardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreDisplay;
    [SerializeField] private JSONSaving saveManager;

    // Start is called before the first frame update
    void Start()
    {
        highScoreDisplay.SetText(saveManager.LoadData().score.ToString());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
