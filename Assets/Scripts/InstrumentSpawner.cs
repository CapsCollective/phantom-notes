using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InstrumentSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnTime = 10f;
    private float nextSpawn = 0f;

    void SpawnInstrument()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, new Quaternion());
        newEnemy.GetComponent<EnemyInstrument>().Setup((Instrument) Random.Range(0, 3));
        spawnTime = spawnTime /2;
    }

    private void Update()
    {
        if (Time.time >= nextSpawn)
        {
            nextSpawn = Time.time + spawnTime;
            if (spawnTime > 5f)
                spawnTime -= 0.5f;
            if (spawnTime < 5f)
            {
                spawnTime = 5f;
            }
            SpawnInstrument();
        }
    }
}
