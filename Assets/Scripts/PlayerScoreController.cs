using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerScoreController : MonoBehaviour
{
    private float maxHealth = 100f;
    private float health = 100f;
    private int score = 0;

    public PostProcessProfile profile;

    void Start()
    {
        profile.GetSetting<Vignette>().intensity.value = 0.28f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        health += Time.deltaTime;
        if (health > maxHealth)
            health = maxHealth;
    }

    public float GetHealth() { return health; }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }

        profile.GetSetting<Vignette>().intensity.value = map(health, 0, 100, 1, 0.28f);
    }

    private void Die()
    {
        print("Player is dead, return to menu");
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

}
