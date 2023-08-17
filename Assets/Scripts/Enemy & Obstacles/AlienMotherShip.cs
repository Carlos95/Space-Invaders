using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMotherShip : Enemy
{

    protected override void Awake()
    {
        base.Awake();
        foreach (GameObject wp in GameObject.FindGameObjectsWithTag("MotherShipWaypoint"))
        {
            waypoints.Add(wp);
        }
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        speed = 1f;
        healthPoints = 500;
        scoreValue = healthPoints;
        StartCoroutine(RandomSpeed());
    }

    IEnumerator RandomSpeed()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);
            speed = Random.Range(0, 5);
        }
    }
}
