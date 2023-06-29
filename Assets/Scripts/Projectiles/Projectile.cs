using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    private float topBound = 20;
    private float bottomBound = -10;
    private int m_Damage;
    public int damage
    {
        get { return m_Damage; }
        set
        {
            if (value < 0.0f)
            {
                Destroy(gameObject);
                Debug.Log("Kill enemy if HP below 0");
            }
            else
            {
                m_Damage = value;
            }
        }
    }

    private float m_Speed;
    public float speed
    {
        get { return m_Speed; }
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("Enemy can't have a negative moving speed!");
            }
            else
            {
                m_Speed = value;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OutOfBounds();
    }
    

    void OutOfBounds()
    {
        if (transform.position.y > topBound || transform.position.y < bottomBound)
        {
            gameObject.SetActive(false);
        }
    }

    protected abstract void Move();
}
