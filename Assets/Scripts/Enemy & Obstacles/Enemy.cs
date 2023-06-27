using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{
    private float m_Speed = 1;
    private int m_HealthPoints;
    [SerializeField] protected List<Transform> waypoints;
    private int m_nextPosition;
    private Vector2 m_CurrentPosition;
    private Vector2 m_TargetPosition;

    public int healthPoints
    {
        get { return m_HealthPoints; }
        set
        {
            if (value <= 0.0f)
            {
                Destroy(gameObject);
                Debug.Log("Enemy Killed!");
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
            if (value < 0.0f)
            {
                Debug.LogError("Enemy can't have a negative moving speed!");
            } else
            {
                m_Speed = value;
            }
        }
    }

    private void Start()
    {
        if (!waypoints.Any() || waypoints[m_nextPosition] == null)
        {
            Debug.LogError("The enemy waypoint system is not working");
        }
    }

    private void Update()
    {
        EnemyMovePattern();
    }
    

    public virtual void EnemyMovePattern()
    {
        if (waypoints.Count == 0) return;
        m_CurrentPosition = transform.position;
        m_TargetPosition = waypoints[m_nextPosition].position;
        // Distance between enemy and waypoint
        float distance = Vector2.Distance(m_CurrentPosition, m_TargetPosition);
        Vector2 directionOfTravel = m_TargetPosition - m_CurrentPosition;
        directionOfTravel.Normalize();

        transform.Translate(directionOfTravel.x * m_Speed * Time.deltaTime, directionOfTravel.y * m_Speed * Time.deltaTime, 0, Space.World);

        if (distance > 0.1f) return;
        m_nextPosition++;
        if (m_nextPosition < waypoints.Count) return;
        m_nextPosition = 0;
    }
}
