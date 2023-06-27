using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed = 20.0f;
    public float xBoundary;
    public float yBoundary;
    public GameObject deathFX;

    [SerializeField] private List<GameObject> heartContainers;
    [SerializeField] private List<Image> heartFills;
    private int remainingHearts;


    private bool canShoot;
    private bool isInvulnerable;

    // variables used for wave animations
    float elapsedTime = 0f;
    float frequency = 3f;

    SpriteRenderer playerSprite;
    Color playerColor;

    private void GetInitialNumberOfHearts()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty serializedProperty = serializedObject.FindProperty("heartContainers");

        remainingHearts = serializedProperty.arraySize;
    }
    private void Start()
    {
        canShoot = true;
        isInvulnerable = false;
        playerSprite = GetComponent<SpriteRenderer>();
        playerColor = playerSprite.color;
        GetInitialNumberOfHearts();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerLimits();
        Shoot();
        SetHealth();

        if (isInvulnerable)
        {
            ActivateInvulnerabilityAnimation();
        } 

        if (IsDead())
        {
            ActivateExplosionAnimation();
        }
    }

    void PlayerLimits()
    {
        if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector2(-xBoundary, transform.position.y);
        }

        if (transform.position.x > xBoundary)
        {
            transform.position = new Vector2(xBoundary, transform.position.y);
        }

        if (transform.position.y < -yBoundary)
        {
            transform.position = new Vector2(transform.position.x, -yBoundary);
        }

        if (transform.position.y > yBoundary)
        {
            transform.position = new Vector2(transform.position.x, yBoundary);
        }
    }

    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector2.up * Time.deltaTime * speed * verticalInput);
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            if (canShoot)
            {

                StartCoroutine(BulletCadence());
            }
            else
            {
                StopCoroutine(BulletCadence());
            }
        }
         
    }

    IEnumerator BulletCadence()
    {
        canShoot = false;
        Vector3 bulletOffset = new Vector3(0, 2);
        GameObject pooledProjectile = ProjectilePooler.SharedInstance.GetPooledObject();
        pooledProjectile.SetActive(true); // activate it
        pooledProjectile.transform.position = transform.position + bulletOffset; // position it at player
        yield return new WaitForSeconds(0.2f);
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
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Projectile") && !isInvulnerable) {
            ReduceHealthPoint();
        }
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
