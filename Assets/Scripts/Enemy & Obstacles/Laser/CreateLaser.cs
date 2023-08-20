using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public GameObject laserPrefab;
    private GameObject laserInstance;
    Vector3 offset = new Vector2(0, -14);
    // variables used for wave animations
    float elapsedTime = 0f;
    float frequency = 3f;
    private Color laserColor;
    private SpriteRenderer laserSprite;
    private Collider2D laserCollider;
    private float laserTimer = 4;
    private bool showLaser;
    private bool isGracePeriod;

    void Awake()
    {
        laserSprite = GetComponent<CreateLaser>().GetComponent<SpriteRenderer>();

    }
    // Start is called before the first frame update
    void Start()
    {
        showLaser = true;
        isGracePeriod = false;
        laserInstance = (GameObject)Instantiate(laserPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));
        laserSprite = laserInstance.GetComponent<SpriteRenderer>();
        laserCollider = laserInstance.GetComponent<Collider2D>();
        laserColor = laserSprite.color;
        

    }

    // Update is called once per frame
    void Update()
    {
        laserInstance.transform.position = transform.position + offset;

        if (isGracePeriod)
        {
            ActivateGracePeriod();
            laserCollider.enabled = false;
        }  else
        {
            SetLaserAlphaMax();
            laserCollider.enabled = true;
        }

        
    }

    void FixedUpdate() 
    {
        Shoot();
    }

    void OnDestroy()
    {
        Destroy(laserInstance);
    }

    void ActivateGracePeriod()
    {
        elapsedTime += Time.deltaTime * Time.timeScale * frequency;
        laserColor.a = Mathf.Abs(Mathf.Sin(elapsedTime));
        laserSprite.material.SetColor("_Color", laserColor);
    }

    void SetLaserAlphaMax()
    {
        laserColor.a = 1;
        laserSprite.material.SetColor("_Color", laserColor);
    }

    // Method to make laser appear after flashing 3 times and dissappear. Stay some seconds off and then repeat

    void Shoot()
    {
        if (showLaser) {
            StartCoroutine(LaserSwitch());
        } else
        {
            StopCoroutine(LaserSwitch());
        }


    }

    IEnumerator LaserSwitch()
    {
        showLaser = false;
        // Laser is going to appear in 2 seconds
        laserInstance.SetActive(true);
        isGracePeriod = true;
        yield return new WaitForSeconds(2);
        // Laser is active
        isGracePeriod = false;
        yield return new WaitForSeconds(laserTimer-3);
        // Laser will dissappear in 2 seconds
        isGracePeriod = true;
        yield return new WaitForSeconds(2);
        // Laser dissappears for 7 seconds
        laserInstance.SetActive(false);
        yield return new WaitForSeconds(laserTimer);
        showLaser = true;

    }
}
