using UnityEngine;

public class RandomBackgroundManager : MonoBehaviour
{
    public GameObject[] backgroundOptions;

    private void Start()
    {
        ChooseRandomBackground();
    }

    private void ChooseRandomBackground()
    {
        int randomIndex = Random.Range(0, backgroundOptions.Length);
        GameObject chosenObject = backgroundOptions[randomIndex];
        Instantiate(chosenObject, transform.position, Quaternion.identity);
    }
}

