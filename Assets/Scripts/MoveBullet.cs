using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public float speed;
    private float topBound = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed);
        OutOfBounds();
    }

    void OutOfBounds()
    {
        if (transform.position.y > topBound)
        {
            gameObject.SetActive(false);
        }
    }
}
