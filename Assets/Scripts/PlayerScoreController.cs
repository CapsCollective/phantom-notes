using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerScoreController : MonoBehaviour
{
    private float maxHealth = 100f;
    private float health = 100f;
    private int score = 0;

    public AudioClip[] damageSounds;

    public PostProcessProfile profile;

    public TextMesh healthText;

    public AudioSource a;

    public AudioClip deathSound;

    private bool died = false;

    void Start()
    {
        profile.GetSetting<Vignette>().intensity.value = 0.28f;
        healthText.text = ""+(int)health;
        a.volume = 0;
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
        SoundGuy.Instance.PlaySound(Vector3.zero, 1, damageSounds[Random.Range(1, damageSounds.Length)]);
        health -= damageAmount;
        if (health <= 0 && !died)
        {
            Die();
            died = true;
        }

        profile.GetSetting<Vignette>().intensity.value = map(health, 0, 100, 1, 0.28f);
        healthText.text = "" + (int)health;
        a.volume = map(health, 0, 100, 3, 0);
    }

    private void Die()
    {
        print("Player is dead, return to menu");
        StartCoroutine(DelayDeath());
    }

    private IEnumerator DelayDeath ()
    {
        SoundGuy.Instance.PlaySound(Vector3.zero, 1, deathSound, false);
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
        Cursor.lockState = CursorLockMode.None;
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
