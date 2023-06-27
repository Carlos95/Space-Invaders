using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFighter : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        healthPoints = 150;
        canShoot = true;
    }

    private bool canShoot;

    void FixedUpdate()
    {
        Shoot();
    }

    void Shoot()
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

    IEnumerator BulletCadence()
    {
        canShoot = false;
        Vector3 bulletOffset = new Vector3(0, -2);
        GameObject pooledProjectile = ProjectilePooler.SharedInstance.GetPooledObject("Enemy Missile");
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true); // activate it
            pooledProjectile.transform.position = transform.position + bulletOffset; // position it at player
        }
       
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }


}
