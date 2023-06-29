using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // All enemy and obstacles tags
    private const string ALIENFIGHTER_TAG = "AlienFighter";
    private const string MOTHERSHIP_TAG = "MotherShip";
    private const string JUNK_TAG = "Junk";
    private const string ASTEROID_TAG = "Asteroid";

    // The maximum number of enemies or obstacles that can be on the game at once
    private const int MAXCOUNT_ALIENFIGHTER = 3;
    private const int MAXCOUNT_MOTHERSHIP = 1;
    private const int MAXCOUNT_JUNK = 1;
    private const int MAXCOUNT_ASTEROID = 1;



    [SerializeField] private List<GameObject> asteroidPrefabList;
    [SerializeField] private List<GameObject> junkPrefabList;
    [SerializeField] private List<GameObject> alienFighters;
    [SerializeField] private List<GameObject> alienMotherships;
    ScreenBoundaries screenBoundaries;
    private int totalAlienFighters;
    private int totalMothership;
    private int totalAsteroids;
    private int totalJunk;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        screenBoundaries = GameObject.Find("Screen Boundaries").GetComponent<ScreenBoundaries>();
        StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        totalAlienFighters = GetTotalNumberOf(ALIENFIGHTER_TAG);
        totalMothership = GetTotalNumberOf(MOTHERSHIP_TAG);
        totalAsteroids = GetTotalNumberOf(ASTEROID_TAG);
        totalJunk = GetTotalNumberOf(JUNK_TAG);
    }

    void StartSpawning()
    {
        StartCoroutine(SpawnObstacle(asteroidPrefabList, 2, 5, MAXCOUNT_ASTEROID));
        StartCoroutine(SpawnObstacle(junkPrefabList, 10, 12, MAXCOUNT_JUNK));
        StartCoroutine(SpawnObstacle(alienFighters, 2, 5, MAXCOUNT_ALIENFIGHTER));
        StartCoroutine(SpawnObstacle(alienMotherships, 15, 20, MAXCOUNT_MOTHERSHIP));
    }

    int GetTotalNumberOf(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag).Length;
    }

    IEnumerator SpawnObstacle(List<GameObject> prefabList, int minSpawnTime, int maxSpawnTime, int maxNumberofSpawns)
    {
        while (true)
        {
            Vector3 offset = new Vector2(Random.Range(screenBoundaries.leftBoundary, screenBoundaries.rightBoundary), transform.position.y);
            int index = Random.Range(0, prefabList.Count);
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            switch (prefabList[index].tag)
            {
                case ALIENFIGHTER_TAG:
                    if (totalAlienFighters < maxNumberofSpawns)
                    {
                        Instantiate(prefabList[index], transform.position + offset, transform.rotation * Quaternion.Euler(0, 0, 180f));
                    }
                    break;
                case MOTHERSHIP_TAG:
                    if (totalMothership < maxNumberofSpawns)
                    {
                        Instantiate(prefabList[index], transform.position + offset, transform.rotation * Quaternion.Euler(0, 0, 180f));
                    }
                    break;
                case JUNK_TAG:
                    if (totalJunk < maxNumberofSpawns)
                    {
                        Instantiate(prefabList[index], transform.position + offset, transform.rotation * Quaternion.Euler(0, 0, 180f));
                    }
                    break;
                case ASTEROID_TAG:
                    if (totalAsteroids < maxNumberofSpawns)
                    {
                        Instantiate(prefabList[index], transform.position + offset, transform.rotation * Quaternion.Euler(0, 0, 180f));
                    }
                    break;
                default:
                    Debug.Log("Not recognized item should be added to SpawnManager");
                    break;

            }
        }
    }
}
