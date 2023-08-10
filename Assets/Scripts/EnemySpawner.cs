using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

   
    public List<Enemy> enemyList;
    public float radius = 10f;
    public int numberOfEnemy = 3;
    public int spawnInterval = 1;
    public float waveInterval = 5;
    public int waveNumber = 0;
    public Transform playerPos;
    void Start()
    {
        StartCoroutine(SpawnEnemyDelay());

    }
    IEnumerator SpawnEnemyDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(waveInterval);
            waveNumber ++;
            numberOfEnemy ++;
            for (int i = 0; i < numberOfEnemy; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }
        }
           

    }

    private void SpawnEnemy()
    {
        float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);
        Vector3 offset = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        Vector3 spawnPosition = playerPos.position + offset;
        int randomIndex = UnityEngine.Random.Range(0, enemyList.Count);
        Instantiate(enemyList[randomIndex], spawnPosition, Quaternion.identity, transform);
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
