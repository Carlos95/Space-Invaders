using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgainMenuController : MonoBehaviour
{
    public Button replayButton;
    public GameObject replayPanel;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        Button replayButtonComponent = replayButton.GetComponent<Button>(); 
        replayButtonComponent.onClick.AddListener(ResetScene);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        replayPanel.SetActive(false);
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
        Time.timeScale = 0;
    }
}
