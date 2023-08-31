using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundaries : MonoBehaviour
{
    Camera cam;
    [System.NonSerialized] public float leftBoundary;
    [System.NonSerialized] public float rightBoundary;
    [System.NonSerialized] public float topBoundary;
    [System.NonSerialized] public float bottomBoundary;

    private float camDistance;
    private Vector2 bottomCorner;
    private Vector2 topCorner;

    private void Awake()
    {
        GetCameraToWorldPoint();
        CreateScreenBoundaries();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void GetCameraToWorldPoint()
    {
        camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
    }

    void CreateScreenBoundaries()
    {
        leftBoundary = bottomCorner.x;
        rightBoundary = topCorner.x;
        bottomBoundary = bottomCorner.y;
        topBoundary = topCorner.y;
    }
}
