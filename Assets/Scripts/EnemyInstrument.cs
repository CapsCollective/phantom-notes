﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemyInstrument : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float knockBack;
    
    private PlayerController player;
    private float time = 1000;
    private float i = 0;
    private float rate = 0;

    private Instrument instrument;

    [System.Serializable]
    public struct InstrumentToObject
    {
        public Instrument instrumentType;
        public GameObject meshInstrument;
    }

    
    public List<InstrumentToObject> instrumentObjects;
    private GameObject currentInstrumentObject;

    private void Start()
    {
        player = PlayerController.Instance;
    }


    public void Setup (Instrument _instrument)
    {
        instrument = _instrument;

        foreach (InstrumentToObject instrumentObject in instrumentObjects)
            if (instrumentObject.instrumentType == Instrument.FLUTE)
                currentInstrumentObject = instrumentObject.meshInstrument;


        GameObject newObj = Instantiate(currentInstrumentObject, transform);
        Outline newOutline = newObj.AddComponent<Outline>();
        newOutline.OutlineColor = Color.red;
        newOutline.OutlineWidth = 20;
    }

    void Update()
    {
        var velocity = new Vector3();
        rate = 1/time;
        if (i < 1)
        {
            i += Time.deltaTime * rate;
            velocity = Vector3.Lerp(transform.position, player.transform.position, i);
            transform.position = velocity;
        }

        transform.LookAt(player.transform);
        
        if (Input.GetKeyUp(KeyCode.K))
        {
            GameObject newObj = Instantiate(pickupPrefab, transform.position, transform.rotation);
            Destroy(gameObject,0);
            newObj.GetComponent<Rigidbody>().velocity = velocity*knockBack;
            newObj.GetComponent<PickupInstrument>().Setup(instrument, currentInstrumentObject);
        }
    }
}
