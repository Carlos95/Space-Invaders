using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private Vector2 initialPosition;
    public float speed;
    private float repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        LoopBackground();
    }

    void LoopBackground()
    {
        if (transform.position.y < initialPosition.y - repeatWidth)
        {
            transform.position = initialPosition;
        }
    }
}
