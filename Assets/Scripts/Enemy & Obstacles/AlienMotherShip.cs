using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMotherShip : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
        healthPoints = 500;
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
