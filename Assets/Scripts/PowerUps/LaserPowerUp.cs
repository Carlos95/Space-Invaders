using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerUp : PowerUp
{
    
    Vector3 offset = new Vector2(0, 2);

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.PlayAudio(powerUpAudio,audioVolume);
            ShowLaser();
        }
    }

    void ShowLaser()
    {
        player.GetComponent<PlayerLaser>().ActivateLaser();
        Destroy(gameObject);
    }
}
