using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{

    protected PlayerController playerController;
    protected GameObject player;
    protected ScoreManager scoreManager;
    private float speed;

    protected AudioManager audioManager;
    [SerializeField] protected AudioClip powerUpAudio;
    protected float audioVolume = 0.5f;

    
    void Awake()
    { 
        player = GameObject.Find("Player");
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        speed = 0.2f;
    }
    
    // Start is called before the first frame update
    protected void Start()
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
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);
}
