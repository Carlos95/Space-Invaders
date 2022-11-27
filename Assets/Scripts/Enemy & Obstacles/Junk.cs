using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junk : Obstacle
{
    private Rigidbody2D junkRb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        junkRb = GetComponent<Rigidbody2D>();
        junkRb.mass = 5;
        junkRb.drag = 1;
        junkRb.angularDrag = 2;
        junkRb.gravityScale = 0;

        player = GameObject.Find("Player");
        StartCoroutine(DestroyTimeout());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Move()
    {
        junkRb.AddForce((player.transform.position - transform.position).normalized * 2,ForceMode2D.Impulse);
    }

    protected override IEnumerator DestroyTimeout()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
