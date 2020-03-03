using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemyInstrument : MonoBehaviour
{
    public AudioClip[] deathSounds;
    public GameObject pickupPrefab;
    public float knockBack;
    private AudioSource audioSource;
    
    
    private PlayerController player;
    private float time = 1000;
    private float i = 0;
    private float rate = 0;
    private bool died = false;

    private Instrument instrument;
    
    private int[] healthValues = new int[] {10, 20, 30, 5};

    private int health;
    
    private float seed;

    public float minYFloor;

    public Rigidbody rb;

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
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = deathSounds[(int) instrument];
        health = healthValues[(int) instrument];
        player = PlayerController.Instance;
        seed = Random.Range(0, 100);
    }


    public void Setup (Instrument _instrument)
    {
        instrument = _instrument;

        foreach (InstrumentToObject instrumentObject in instrumentObjects)
            if (instrumentObject.instrumentType == Instrument.Flute)
                currentInstrumentObject = instrumentObject.meshInstrument;


        GameObject newObj = Instantiate(currentInstrumentObject, transform);
        Outline newOutline = newObj.AddComponent<Outline>();
        newOutline.OutlineColor = Color.red;
        newOutline.OutlineWidth = 20;
    }

    void Update()
    {
        Vector3 newPos = new Vector3();
        float wavyRange = 5f;
        float wavySpeed = 3;

        rate = 1/time;
        if (i < 1)
        {
            i += Time.deltaTime * rate;
            //newPos = Vector3.Lerp(transform.position, player.transform.position + randomNoise, i);
            //newPos.y = Mathf.Clamp(newPos.y, minYFloor, float.MaxValue);

            //transform.position = newPos;
        }

        Vector3 randomNoise = new Vector3(Mathf.Sin((Time.time* wavySpeed) + seed) * wavyRange, Mathf.Cos((Time.time* wavySpeed) + (seed*2)) * wavyRange, Mathf.Sin((Time.time* wavySpeed) + seed) * wavyRange);
        rb.AddForce(player.transform.position - transform.position);
        rb.AddForce(randomNoise);

        transform.LookAt(player.transform);

        if (health <= 0 && !died)
        {
            died = true;
            audioSource.Play();
            GameObject newObj = Instantiate(pickupPrefab, transform.position, transform.rotation);
            newObj.transform.SetParent(null);
            Destroy(gameObject, audioSource.clip.length);
            newObj.GetComponent<Rigidbody>().velocity = newPos * knockBack;
            newObj.GetComponent<PickupInstrument>().Setup(instrument, currentInstrumentObject);
            transform.position = Vector3.one * 9999f;
        }
    }

    public void Damage(int value)
    {
        health -= value;
    }
}
