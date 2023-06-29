using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    // All enemy and obstacles tags
    private const string ALIENFIGHTER_TAG = "AlienFighter";
    private const string MOTHERSHIP_TAG = "MotherShip";
    private const string JUNK_TAG = "Junk";
    private const string ASTEROID_TAG = "Asteroid";

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.2f;
        damage = 25;
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

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //if the collision is between the projectile and an obstacle/enemy
        if (other.gameObject.CompareTag(JUNK_TAG) 
            || other.gameObject.CompareTag(ASTEROID_TAG))
        {
            gameObject.SetActive(false);
            other.gameObject.GetComponent<Obstacle>().healthPoints -= damage;
        }

        if (other.gameObject.CompareTag(ALIENFIGHTER_TAG) 
            || other.gameObject.CompareTag(MOTHERSHIP_TAG))
        {
            gameObject.SetActive(false);
            other.gameObject.GetComponent<Enemy>().healthPoints -= damage;
        }
    }
}
