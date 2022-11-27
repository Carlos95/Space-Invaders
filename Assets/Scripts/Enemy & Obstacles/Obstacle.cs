using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    private float m_Speed = 1;
    private int m_HealthPoints;
    public int healthPoints
    {
        get { return m_HealthPoints; }
        set
        {
            if (value < 0.0f)
            {
                Destroy(gameObject);
                Debug.LogError("Obstacle can't have a negative moving speed!");
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

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();

    protected  virtual void OnTriggerEnter2D(Collider2D other)
    {
        //if the collision is between the obstacle and a projectile
        if (other.gameObject.CompareTag("Projectile"))
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    protected abstract IEnumerator DestroyTimeout();
    
}
