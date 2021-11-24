using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPooler : MonoBehaviour
{
    public static BulletsPooler SharedInstance;
    private Bullet[] _pooledObjects;
    public Bullet objectToPool;
    public int amountToPool = 20;
  
    void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        _pooledObjects = new Bullet[amountToPool];
        for (int i = 0; i < amountToPool; i++)
        {
            var bullet = Instantiate(objectToPool,transform);
            bullet.gameObject.SetActive(false);
            _pooledObjects[i] = bullet;
        }
    }

    public Bullet GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Length; i++)
        {
            if (!_pooledObjects[i].gameObject.activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        
        return null;
    }
}