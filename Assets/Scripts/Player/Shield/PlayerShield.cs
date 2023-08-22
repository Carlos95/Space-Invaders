using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{

    [SerializeField] private GameObject shield;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip shieldAudio;

    // Start is called before the first frame update
    void Start()
    {
        DeactivateShield();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateShield()
    {
        StartCoroutine(IEActivateShield());
    }

    IEnumerator IEActivateShield()
    {
        shield.SetActive(true);
        audioManager.PlayAudio(shieldAudio, 0.7f);
        yield return new WaitForSeconds(4);
        DeactivateShield();
    }

    public void DeactivateShield()
    {
        shield.SetActive(false);
    }

    public bool IsActive()
    {
        return shield.activeSelf;
    }
}
