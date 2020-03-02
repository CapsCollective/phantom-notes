using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
    private float health = 100f;
    private int score = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetHealth() { return health; }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        print("Player is dead, return to menu");
    }
}
