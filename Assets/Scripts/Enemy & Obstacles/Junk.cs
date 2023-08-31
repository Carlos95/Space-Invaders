using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junk : Obstacle
{
    private Rigidbody2D junkRb;
    //private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        junkRb = GetComponent<Rigidbody2D>();
        junkRb.mass = 5;
        junkRb.drag = 1;
        junkRb.angularDrag = 2;
        junkRb.gravityScale = 0;

        speed = 2;
        healthPoints = 25;
        scoreValue = healthPoints;

        //player = GameObject.Find("Player");
        StartCoroutine(DestroyTimeout());
    }

    

    protected override void MoveWithForce()
    {
        junkRb.AddForce((playerController.transform.position - transform.position).normalized * speed, ForceMode2D.Impulse);
    }

    protected override IEnumerator DestroyTimeout()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    } 
}
