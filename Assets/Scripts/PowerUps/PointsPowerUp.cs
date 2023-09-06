using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPowerUp : PowerUp
{
    private const int SCOREVALUE = 1000;
    // Start is called before the first frame update

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.PlayAudio(powerUpAudio,audioVolume);
            scoreManager.AddScore(SCOREVALUE);
            Destroy(gameObject);
        }
    }
}
