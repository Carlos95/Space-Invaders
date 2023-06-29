using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public GameObject laserPrefab;
    private GameObject laserInstance;
    Vector3 offset = new Vector2(0, -14);

    void Awake()
    {
        

    }
    // Start is called before the first frame update
    void Start()
    {
        laserInstance = (GameObject)Instantiate(laserPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        laserInstance.transform.position = transform.position + offset;
    }

    void OnDestroy()
    {
        Destroy(laserInstance);
    }

    // Method to make laser appear after flashing 3 times and dissappear. Stay some seconds off and then repeat
}
