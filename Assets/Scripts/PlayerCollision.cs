using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private PlayerScoreController healthScore;

    void Start()
    {
        healthScore = GetComponent<PlayerScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Played collided with: " + collision.gameObject.name);
        if (collision.gameObject.tag == "enemy")//  Player is hit by enemy, kill enemy and damage player
        {
            Destroy(collision.gameObject);
            healthScore.TakeDamage(25);
        }
        if (collision.gameObject.tag == "pickup")
        {
            print("Player picks up a: ");
        }
    }
}
