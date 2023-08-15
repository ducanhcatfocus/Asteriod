using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager _instance;

    public static PoolingManager Instance => _instance;



    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;

        }
    }

   
    public Dictionary<Type, List<IPoolable>> objPools = new Dictionary<Type, List<IPoolable>>();

    public T GetFromPool<T>(T poolPrefab) where T :  Component, IPoolable
    {
        Type objType = typeof(T);

        if (!objPools.ContainsKey(objType))
        {
            objPools[objType] = new List<IPoolable>();
        }
        T target = null;

        foreach (T objPool in objPools[objType])
        {
           
                if ((objPool as MonoBehaviour).gameObject.activeSelf == false)
                {
                    Debug.Log(objPool);
                    target = objPool;
                    break;

                }
            
    
        }

        if (target == null)
        {
            T newObj = Instantiate(poolPrefab, transform);
            objPools[objType].Add(newObj);
            target = newObj;

        }
        target.ResetElapsedTime();
        target.gameObject.SetActive(true);
        return target;
    }


}
