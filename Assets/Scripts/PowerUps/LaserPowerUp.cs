using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerUp : PowerUp
{

    public GameObject laserPrefab;
    private GameObject laserInstance;
    Vector3 offset = new Vector2(0, 2);


    // Start is called before the first frame update
    void Start()
    {
        //laserInstance = (GameObject)Instantiate(laserPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));
        //laserInstance.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //laserInstance.transform.position = player.transform.position + offset;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowLaser();
        }
    }

    void ShowLaser()
    {
        //laserInstance.SetActive(true);
    }
}
