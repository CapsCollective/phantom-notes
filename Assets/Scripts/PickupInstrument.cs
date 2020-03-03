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
    public AudioClip[] pickupSounds;
    
    private PlayerController player;

    private Instrument instrument;

    private void Start()
    {
        player = PlayerController.Instance;
        Invoke(nameof(Disappear), secondsToDisappear);
    }

    public void Setup (Instrument _instrument, GameObject _meshInstrument)
    {
        instrument = _instrument;

        Instantiate(_meshInstrument, transform);

    }
    
    void Update()
    {
        var playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (playerDistance < pickupDistance)
        {
            SoundGuy.Instance.PlaySound(Vector3.zero, 1, pickupSounds[(int) instrument]);
            Destroy(gameObject, 0);
            PlayerWeaponController.Instance.AddAmmo(instrument);
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
