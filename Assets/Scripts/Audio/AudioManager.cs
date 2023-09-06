using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] backgroundAudio;
    private GameObject mainMenuAudioSource;

    private void Awake()
    {
        mainMenuAudioSource =  GameObject.Find("MainMenuAudioSource");
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(mainMenuAudioSource);
        audioSource = GetComponent<AudioSource>();
        PlayRandomBackgroundAudio();
    }

    public void PlayAudio(AudioClip audio, float volume)
    {
        audioSource.PlayOneShot(audio, volume);
    }

    public void PlayAudio(AudioClip[] audioClips, float volume)
    {
        if (audioClips.Length > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Length);
            audioSource.PlayOneShot(audioClips[randomIndex], volume);
        }
        else
        {
            Debug.LogError("No audio clips assigned.");
        }
    }

    private void PlayRandomBackgroundAudio()
    {
        if (backgroundAudio.Length > 0)
        {
            int randomIndex = Random.Range(0, backgroundAudio.Length);
            audioSource.clip = backgroundAudio[randomIndex];
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No audio clips assigned.");
        }
    }
}
