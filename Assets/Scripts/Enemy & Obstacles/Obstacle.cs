using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    private float m_Speed = 1;
    private int m_HealthPoints;
    private PlayerController m_Player;
    private ScoreManager scoreManager;
    public int scoreValue { get; set; }
    public PlayerController player
    {
        get { return m_Player; }
        set
        {
            m_Player = value;
        }
    }

    public int healthPoints
    {
        get { return m_HealthPoints; }
        set
        {
            if (value <= 0.0f)
            {
                Destroy(gameObject);
                scoreManager.AddScore(scoreValue);
                scoreManager.ShowAdditionScore(scoreValue);
                Debug.Log("Obstacle Killed!");
            }
            else
            {
                m_HealthPoints = value;
            }
        }
    }

    public float speed
    {
        get { return m_Speed; }
        set
        {
            if (value <= 0.0f)
            {
                Debug.LogError("Obstacle can't have a negative moving speed!");
            }
            else
            {
                m_Speed = value;
            }
        }
    }

    private void Awake()
    {
        try
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        catch
        {
            Debug.Log("Player Not Found");
        };

        try
        {
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        } catch
        {
            Debug.Log("Score Manager not found");
        }
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (player != null && !player.IsDead())
        {
            Move();
        }        
    }

    protected abstract void Move();
    

    protected abstract IEnumerator DestroyTimeout();
    
}
