using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{

    protected PlayerController playerController;
    protected GameObject player;
    private float speed;

    
    void Awake()
    {
        try
        {
            player = GameObject.Find("Player");
            playerController = player.GetComponent<PlayerController>();
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
        if (player != null && !playerController.IsDead())
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.down * speed);
    }

    private IEnumerator DestroyTimeout()
    {
        Debug.LogError("Start destroy countdown");
        yield return new WaitForSeconds(5);
        Debug.LogError("Destroyed");

        Destroy(gameObject);
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);


}
