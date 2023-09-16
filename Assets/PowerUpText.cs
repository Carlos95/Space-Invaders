using System.Collections;
using UnityEngine;
using TMPro;

public class PowerUpText : MonoBehaviour
{
    [SerializeField] private TMP_Text powerUpText;
    [SerializeField] private GameObject powerUpDisplay;

    
    // Start is called before the first frame update
    void Start()
    {
        powerUpDisplay.SetActive(false);
    }

    public void ActivateText(string text)
    {
        StartCoroutine(ShowText(text));
    }
    
    IEnumerator ShowText(string text)
    {
        powerUpDisplay.SetActive(true);
        powerUpText.SetText(text);
        yield return new WaitForSeconds(2);
        powerUpDisplay.SetActive(false);
    }
}
