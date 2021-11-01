using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPooler : MonoBehaviour
{
    public static BulletsPooler SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public GameObject pistolMuzzle;
    

    public int amountToPool;
  
    void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool,pistolMuzzle.transform.position , pistolMuzzle.transform.rotation);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(pistolMuzzle.transform);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
