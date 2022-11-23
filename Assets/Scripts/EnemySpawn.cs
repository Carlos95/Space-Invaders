using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public List<GameObject> enemyPrefabList = new List<GameObject>();
    PlayerController playerController; 
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            Vector3 offset = new Vector2(Random.Range(-playerController.xBoundary, playerController.xBoundary),transform.position.y);
            int index = Random.Range(0, enemyPrefabList.Count);
            yield return new WaitForSeconds(2);
            Instantiate(enemyPrefabList[index], transform.position + offset, transform.rotation);
        }
    }
}
