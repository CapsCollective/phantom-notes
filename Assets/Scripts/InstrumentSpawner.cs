using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnInstrument), 2.0f, 3f);
    }

    void SpawnInstrument()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, new Quaternion());
        newEnemy.GetComponent<EnemyInstrument>().Setup(Instrument.FLUTE); /// <----- CHANGE TO SELECT INSTRUMENT
    }
}
