using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : Projectile
{
    [SerializeField] private AudioClip audioOnShieldImpact;
    [SerializeField] private GameObject shieldImpactAnimation;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    

    protected override void Move()
    {
        transform.Translate(Vector2.up * speed);
    }

    private void ActivateShieldImpactAnimation()
    {
        Instantiate(shieldImpactAnimation, transform.position, transform.rotation);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        } else if (other.gameObject.CompareTag("Shield"))
        {
            ActivateShieldImpactAnimation();
            audioManager.PlayAudio(audioOnShieldImpact,0.9f);
            gameObject.SetActive(false);
        }
    }
}
