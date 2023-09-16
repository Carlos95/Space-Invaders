using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    /*
    private float horizontalInput;
    private float verticalInput;
    private float speed = 20.0f;
    */

    public GameObject deathFX;
    public AudioClip shootingAudio;
    public AudioClip onDeathAudio;
    public AudioClip[] onSelfHitAudio;

    ScreenBoundaries screenBoundaries;
    private float objectWidth;
    private float objectHeight;
    private float leftBoundary;
    private float rightBoundary;
    private float bottomBoundary;
    private float topBoundary;

    [SerializeField] private List<GameObject> heartContainers;
    [SerializeField] private List<Image> heartFills;
    [SerializeField] private PlayerLaser playerLaser;
    [SerializeField] private PlayerShield playerShield;
    [SerializeField] private AudioManager audioManager;
    public int remainingHearts;

    private bool canShoot;
    private float bulletCadence = 0.4f;
    private bool isInvulnerable;

    // variables used for wave animations
    float elapsedTime = 0f;
    float frequency = 3f;

    SpriteRenderer playerSprite;
    Color playerColor;

    
    private void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        objectWidth = playerSprite.bounds.size.x / 2;
        objectHeight = playerSprite.bounds.size.y / 2;        
        screenBoundaries = GameObject.Find("Screen Boundaries").GetComponent<ScreenBoundaries>();
    }

    private void Start()
    {
        canShoot = true;
        isInvulnerable = false;
        playerColor = playerSprite.color;
        GetBounderies();
        remainingHearts = GetInitialNumberOfHearts();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLimitCorrection();
        Shoot();
        SetHealth();

        if (isInvulnerable)
        {
            ActivateInvulnerabilityAnimation();
        } 

        if (IsDead())
        {
            audioManager.PlayAudio(onDeathAudio,0.7f);
            ActivateExplosionAnimation();
            playerLaser.DeactivateLaser();
        }
    }

    public int GetInitialNumberOfHearts()
    {
        return heartContainers.Count;
    }

    void PlayerLimitCorrection()
    {
        Vector2 pos = transform.position;
        
        if (pos.x < leftBoundary)
        {
            transform.position = new Vector2(leftBoundary, transform.position.y);
        }

        if (pos.x > rightBoundary)
        {
            transform.position = new Vector2(rightBoundary, transform.position.y);
        }

        if (pos.y < bottomBoundary)
        {
            transform.position = new Vector2(transform.position.x, bottomBoundary);
        }

        if (pos.y > topBoundary)
        {
            transform.position = new Vector2(transform.position.x, topBoundary);
        }
    }

    void GetBounderies()
    {
        leftBoundary = screenBoundaries.leftBoundary + objectWidth;
        rightBoundary = screenBoundaries.rightBoundary - objectWidth;
        bottomBoundary = screenBoundaries.bottomBoundary + objectHeight;
        topBoundary = screenBoundaries.topBoundary - objectHeight;
    }

    void Shoot()
    {
        if (!IsDead() && !playerLaser.IsActive())
        {

            if (canShoot)
            {
                audioManager.PlayAudio(shootingAudio,0.05f);
                StartCoroutine(WithBulletCadence());
            }
            else
            {
                StopCoroutine(WithBulletCadence());
            }
        }
    }

    IEnumerator WithBulletCadence()
    {
        canShoot = false;
        Vector3 bulletOffset = new Vector3(0, 1);
        GameObject pooledProjectile = ProjectilePooler.SharedInstance.GetPooledObject("Player Missile");
        pooledProjectile.SetActive(true); // activate it
        pooledProjectile.transform.position = transform.position + bulletOffset; // position it at player
        yield return new WaitForSeconds(bulletCadence);
        canShoot = true;
    }

    void SetHealth()
    {
        foreach(Image healthHeart in heartFills)
        {
            int heartNumber = heartFills.IndexOf(healthHeart) + 1;
            
            // Set hearts over the remaining amount of hearts OFF
            if ( heartNumber > remainingHearts )
            {
                healthHeart.fillAmount = 0;
            } else if (heartNumber <= remainingHearts )
            {
                healthHeart.fillAmount = 1;
            }
        }
    }

    public bool IsDead()
    {
        if (remainingHearts <= 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    void ActivateInvulnerabilityAnimation()
    {
        elapsedTime += Time.deltaTime * Time.timeScale * frequency;
        playerColor.a = Mathf.Abs(Mathf.Sin(elapsedTime));
        playerSprite.material.SetColor("_Color", playerColor);
    }

    void ActivateExplosionAnimation()
    {
        Instantiate(deathFX, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isHarmful(other)) {
            ReduceHealthPoint();
            audioManager.PlayAudio(onSelfHitAudio, 0.6f);
        }
    }

    private bool isHarmful(Collider2D other)
    {
        return !other.CompareTag("Projectile") 
            && !other.CompareTag("PowerUp")
            && !other.CompareTag("PlayerLaser")
            && !isInvulnerable
            && !playerShield.IsActive();
    }

    void ReduceHealthPoint()
    {
        StartCoroutine(SetInvulnerability());
        remainingHearts--;
    }

    IEnumerator SetInvulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(3);
        isInvulnerable = false;
        playerColor.a = 1;
        playerSprite.material.SetColor("_Color", playerColor);
    }
}
