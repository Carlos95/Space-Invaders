using UnityEngine;

public class PointsPowerUp : PowerUp
{
    private const int SCOREVALUE = 1000;


    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.PlayAudio(powerUpAudio,audioVolume);
            scoreManager.AddScore(SCOREVALUE);
            ActivateDescription();
            Destroy(gameObject);
        }
    }

    private void ActivateDescription()
    {
        player.GetComponent<PowerUpText>().ActivateText("+1000 points");
    }
}
