using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInstrument : MonoBehaviour
{
    public float pickupDistance;
    public float hooverDistance;
    public float hooverSpeed;
    
    private PlayerController player;

    private void Start()
    {
        player = PlayerController.Instance;
    }
    
    void Update()
    {
        var playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (playerDistance < pickupDistance)
        {
            Destroy(gameObject,0);
            print("picked up item");
        }
        else if (playerDistance < hooverDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                player.transform.position, 
                hooverSpeed * Time.deltaTime);
        }
    }
}
