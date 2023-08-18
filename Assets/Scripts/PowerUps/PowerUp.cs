using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{

    protected PlayerController player;
    private float speed;

    
    void Awake()
    {
        try
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        catch
        {
            Debug.Log("Player Not Found");
        };
        speed = 0.2f;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyTimeout());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null && !player.IsDead())
        {
            Move();
        }
    }

    private void Move()
    {
        Debug.Log("Moving");

        transform.Translate(Vector2.down * speed);
    }

    private IEnumerator DestroyTimeout()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);


}
