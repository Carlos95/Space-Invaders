using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> asteroidPrefabList;
    [SerializeField] private List<GameObject> junkPrefabList;
    [SerializeField] private List<GameObject> alienFighters;
    [SerializeField] private List<GameObject> alienMotherships;
    PlayerController playerController; 
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(SpawnObstacle(asteroidPrefabList, 2, 5));
        StartCoroutine(SpawnObstacle(junkPrefabList, 4, 6));
        StartCoroutine(SpawnObstacle(alienFighters, 2, 5));
        StartCoroutine(SpawnObstacle(alienMotherships, 15, 20));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnObstacle(List<GameObject> prefabList, int minSpawnTime, int maxSpawnTime)
    {
        while(true)
        {
            Vector3 offset = new Vector2(Random.Range(-playerController.xBoundary, playerController.xBoundary),transform.position.y);
            int index = Random.Range(0, prefabList.Count);
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            Instantiate(prefabList[index], transform.position + offset, transform.rotation * Quaternion.Euler(0,0,180f));
        }
    }
}
