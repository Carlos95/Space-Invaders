using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSetup : MonoBehaviour
{
    private float leftBoundary;
    private float rightBoundary;
    private float bottomBoundary;
    private float topBoundary;
    ScreenBoundaries screenBoundaries;

    private const float OFFSET = 1f; 

    private void Awake()
    {
        screenBoundaries = GameObject.Find("Screen Boundaries").GetComponent<ScreenBoundaries>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetBoundaries();
        GiveCoords();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetBoundaries()
    {
        leftBoundary = screenBoundaries.leftBoundary+OFFSET;
        rightBoundary = screenBoundaries.rightBoundary-OFFSET;
        bottomBoundary = screenBoundaries.bottomBoundary;
        topBoundary = screenBoundaries.topBoundary-OFFSET;
    }

    void GiveCoords()
    {
       switch (name)
       {
            case "C_WP":
                transform.position = new Vector2(leftBoundary, topBoundary);
                break;
            case "D_WP":
                transform.position = new Vector2(rightBoundary, topBoundary);
                break;
            case "A_WP":
                transform.position = new Vector2(leftBoundary, bottomBoundary / 3);
                break;
            case "B_WP":
                transform.position = new Vector2(rightBoundary, bottomBoundary / 3);
                break;
            case "WP_MOTHERSHIP_LEFT":
                transform.position = new Vector2(leftBoundary, topBoundary / 3);
                break;
            case "WP_MOTHERSHIP_RIGHT":
                transform.position = new Vector2(rightBoundary, topBoundary / 3);
                break;
            default:
                transform.position = new Vector2(leftBoundary, topBoundary);
                break;
       }
    }
}
