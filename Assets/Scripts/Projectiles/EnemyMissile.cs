using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : Projectile
{
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

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
