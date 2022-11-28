using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.2f;
        healthPoints = 50;
        StartCoroutine(DestroyTimeout());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Move()
    {
        transform.Translate(Vector2.up * speed);
    }

    protected override IEnumerator DestroyTimeout()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}