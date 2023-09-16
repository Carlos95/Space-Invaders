using UnityEngine;

public class ShieldPowerUp : PowerUp
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerShield>().ActivateShield();
            Destroy(gameObject);
        }
    }
}
