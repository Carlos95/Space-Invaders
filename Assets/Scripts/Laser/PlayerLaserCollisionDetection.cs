using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserCollisionDetection : MonoBehaviour
{
    // All enemy and obstacles tags
    private const string ALIENFIGHTER_TAG = "AlienFighter";
    private const string MOTHERSHIP_TAG = "MotherShip";
    private const string JUNK_TAG = "Junk";
    private const string ASTEROID_TAG = "Asteroid";

    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        //if the collision is between the projectile and an obstacle/enemy
        if (other.gameObject.CompareTag(JUNK_TAG)
            || other.gameObject.CompareTag(ASTEROID_TAG))
        {
            other.gameObject.GetComponent<Obstacle>().healthPoints -= damage;
        }

        if (other.gameObject.CompareTag(ALIENFIGHTER_TAG)
            || other.gameObject.CompareTag(MOTHERSHIP_TAG))
        {
            other.gameObject.GetComponent<Enemy>().healthPoints -= damage;
        }
    }
}
