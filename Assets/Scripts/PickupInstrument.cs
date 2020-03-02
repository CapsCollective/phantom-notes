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
    private float i = 0;
    private float rate = 0;

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
            rate = 1/hooverSpeed;
            if (i < 1)
            {
                i += Time.deltaTime * rate;
                transform.position = Vector3.Lerp(transform.position, player.transform.position, i);
            }
        }
    }
}
