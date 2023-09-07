using UnityEngine;
using System.Collections;
using TMPro;
using Dan.Main;
using UnityEngine.Events;
public class InputPlayerName : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private GameObject emptyNameErrorText;
    [SerializeField] private GameObject duplicateErrorText;
    [SerializeField] private GameObject successNameChangeText;
    [SerializeField] private JSONSaving saveManager;
    private PlayerData playerData;

    private string publicLeaderboardKey = "4da4217571108f6e4f81b99faeae1fb7c64162fa2c9cb2d13e527db217fe61a1";
    public UnityEvent successfullNameChangeEvent;

    private void Awake()
    {
        emptyNameErrorText.SetActive(false);
        successNameChangeText.SetActive(false);
        duplicateErrorText.SetActive(false);
    }

    private void Start()
    {
        playerData = saveManager.LoadData();
    }

    public void SaveName()
    {
        if (inputName.text != "")
        {
            emptyNameErrorText.SetActive(false);
            UpdateName(inputName.text);
        } else
        {
            emptyNameErrorText.SetActive(true);
        }
    }

    IEnumerator ShowNameChangeSuccess()
    {
        successNameChangeText.SetActive(true);
        yield return new WaitForSeconds(3);
        successNameChangeText.SetActive(false);
    }



    
    public void UpdateName(string username)
    {
        LeaderboardCreator.UpdateEntryUsername(publicLeaderboardKey, username, (msg) =>
        {
            if (msg)
            {
                duplicateErrorText.SetActive(false);
                StartCoroutine(ShowNameChangeSuccess());
                playerData.name = inputName.text;
                saveManager.SaveData(playerData);
                successfullNameChangeEvent.Invoke();
            } else
            {
                duplicateErrorText.SetActive(true);
            }
            
        });
    }
}
