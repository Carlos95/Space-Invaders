using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        Button playButtonComponent = playButton.GetComponent<Button>();
        playButtonComponent.onClick.AddListener(StartGameScene);

        Button exitButtonComponent = exitButton.GetComponent<Button>();
        exitButtonComponent.onClick.AddListener(ExitGameScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    void ExitGameScene()
    {
        Debug.Log("Exiting application");
        Application.Quit();
    }
}
