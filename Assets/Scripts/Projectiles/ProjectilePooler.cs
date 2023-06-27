using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    public static ProjectilePooler SharedInstance;
    private List<GameObject> pooledPlayerMissiles;
    private List<GameObject> pooledEnemyMissiles;
    [SerializeField] private List<GameObject> objectsToPool;
    [SerializeField] private int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Loop through list of pooled objects,deactivating them and adding them to the list 
        pooledPlayerMissiles = new List<GameObject>();
        pooledEnemyMissiles = new List<GameObject>();
        AddObjectsToPool();
        
    }

    public void AddObjectsToPool()
    {
        //List<GameObject> pooledObjects
        foreach (GameObject objectType in objectsToPool)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = Instantiate(objectType);
                obj.SetActive(false);
                if (objectType.name == "Missile")
                {
                    pooledPlayerMissiles.Add(obj);
                }
                else if (objectType.name == "EnemyMissile")
                {
                    pooledEnemyMissiles.Add(obj);
                }
                obj.transform.SetParent(this.transform); // set as children of Spawn Manager
            }
        }
    }

    public GameObject GetPooledObject(string objectType)
    {
        List<GameObject> pooledObjects = new List<GameObject>();
        if (objectType == "Player Missile")
        {
            pooledObjects = pooledPlayerMissiles;
        } else if (objectType == "Enemy Missile")
        {
            pooledObjects = pooledEnemyMissiles;
        } else
        {
            Debug.Log("No object of type " + objectType+" found.");
        }
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // otherwise, return null   
        return null;
    }
} 