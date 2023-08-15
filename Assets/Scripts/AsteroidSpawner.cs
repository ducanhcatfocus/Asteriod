using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnRate = 2f;
    public int spawnAmount = 1;
    public Asteroid asteroidPrefab;
    public float spawnDistance = 15f;
    public float trajectoryVariance = 15f;
    public Transform playerPos;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

   private void Spawn( )
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = playerPos.position + spawnDirection;
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            Asteroid asteroid = PoolingManager.Instance.GetFromPool(asteroidPrefab);
          
                asteroid.transform.position = spawnPoint;
                asteroid.transform.rotation = rotation;
                asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
                asteroid.SetTrajectory(rotation * -spawnDirection);
            
        }
    }
}
