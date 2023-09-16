using UnityEngine;
using UnityEngine.Events;

public class HealthPowerUp : PowerUp
{
    public UnityEvent<string> showPowerUpDescription;
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
            ActivateDescription();
            Destroy(gameObject);
        }
    }

    private void ActivateDescription()
    {
        player.GetComponent<PowerUpText>().ActivateText("+1 heart");
    }
}
