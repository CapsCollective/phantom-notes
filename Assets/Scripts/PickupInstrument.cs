using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInstrument : MonoBehaviour
{
    public float pickupDistance;
    public float hooverDistance;
    public float hooverSpeed;
    public int secondsToDisappear;
    
    private PlayerController player;

    private Instrument thisInstrument;

    private void Start()
    {
        player = PlayerController.Instance;
        Invoke(nameof(Disappear), secondsToDisappear);
    }

    public void Setup (Instrument _instrument, GameObject _meshInstrument)
    {
        thisInstrument = _instrument;

        Instantiate(_meshInstrument, transform);
    }
    
    void Update()
    {
        var playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (playerDistance < pickupDistance)
        {
            Destroy(gameObject,0);
            print("picked up item: " + thisInstrument);
        }
        else if (playerDistance < hooverDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                player.transform.position, 
                hooverSpeed * Time.deltaTime);
        }
    }

    void Disappear()
    {
        Destroy(gameObject,0);
    }
}
