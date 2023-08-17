using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private Vector2 initialPosition;
    public float speed;
    private float repeatWidth;
    private ScoreManager scoreManagerRef;
    private PlayerController playerController;

    void Awake()
    {
        scoreManagerRef = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

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
            AddDistanceScore();
            transform.position = initialPosition;
        }
    }

    void AddDistanceScore()
    {
        if (!playerController.IsDead())
        {
            scoreManagerRef.AddScore(225);
        }
    }
}
