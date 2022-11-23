using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyEnemy());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.down * speed);

    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the collision is between the enemy and a projectile
        if (other.gameObject.CompareTag("Projectile"))
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
