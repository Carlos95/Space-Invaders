using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    private float m_Speed = 1;
    private int m_HealthPoints;
    private PlayerController m_Player;
    private ScoreManager scoreManager;
    private AudioManager audioManager;
    private GameObject player;

    [SerializeField] private AudioClip audioOnDeath;
    [SerializeField] private GameObject deathFX;
    public int scoreValue { get; set; }
    public PlayerController playerController
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
                audioManager.PlayAudio(audioOnDeath,0.4f);
                ActivateExplosionAnimation();
                Destroy(gameObject);
                scoreManager.AddScore(scoreValue);
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

    void Awake()
    {
        player = GameObject.Find("Player");
        if (player != null) {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void ActivateExplosionAnimation()
    {
        Instantiate(deathFX, transform.position, transform.rotation);
    }
    void FixedUpdate()
    {
        if (playerController != null && !playerController.IsDead())
        {
            MoveWithForce();
        }
    }

    void Update()
    {
        if (playerController != null && !playerController.IsDead())
        {
            Move();
        }        
    }

    protected virtual void Move() {}

    protected virtual void MoveWithForce() {}
    

    protected abstract IEnumerator DestroyTimeout();
    
}
