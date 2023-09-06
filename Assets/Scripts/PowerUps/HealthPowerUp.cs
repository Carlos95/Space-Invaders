using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : PowerUp
{   
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IncreaseHealthPoint();
        }
    }

    private void IncreaseHealthPoint()
    {
        if (playerController.remainingHearts < playerController.GetInitialNumberOfHearts() )
        {
            audioManager.PlayAudio(powerUpAudio,audioVolume);
            playerController.remainingHearts++;
            Destroy(gameObject);
        }
    }
}
