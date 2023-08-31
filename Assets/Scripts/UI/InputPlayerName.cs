using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Events;

public class InputPlayerName : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private GameObject emptyNameErrorText;
    [SerializeField] private GameObject successNameChangeText;

    private void Awake()
    {
        emptyNameErrorText.SetActive(false);
        successNameChangeText.SetActive(false);
    }

    public void SaveName()
    {
        if (inputName.text != "")
        {
            emptyNameErrorText.SetActive(false);
            StartCoroutine(ShowNameChangeSuccess());
            SaveManager.SaveString("PlayerName", inputName.text);
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
}
