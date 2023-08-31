using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
        healthPoints = 100;
        scoreValue = healthPoints;
        StartCoroutine(DestroyTimeout());
    }

    protected override void Move()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    protected override IEnumerator DestroyTimeout()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
