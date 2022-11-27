using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> asteroidPrefabList = new List<GameObject>();
    [SerializeField] private List<GameObject> junkPrefabList = new List<GameObject>();
    PlayerController playerController; 
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(SpawnObstacle(asteroidPrefabList));
        StartCoroutine(SpawnObstacle(junkPrefabList));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnObstacle(List<GameObject> prefabList)
    {
        while(true)
        {
            Vector3 offset = new Vector2(Random.Range(-playerController.xBoundary, playerController.xBoundary),transform.position.y);
            int index = Random.Range(0, prefabList.Count);
            yield return new WaitForSeconds(Random.Range(2,5));
            Instantiate(prefabList[index], transform.position + offset, transform.rotation);
        }
    }
}
