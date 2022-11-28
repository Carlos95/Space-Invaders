using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
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
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
            other.gameObject.GetComponent<Obstacle>().healthPoints -= damage;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            other.gameObject.GetComponent<Enemy>().healthPoints -= damage;
        }
    }
}
