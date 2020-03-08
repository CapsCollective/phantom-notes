using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public Camera cam;
    public GameObject[] projectilePrefabs;
    public GameObject[] weaponArray;
    public float[] fireRates;
    public Transform[] projSpawnLocations;
    public int weaponChangeSpeed;
    public AudioClip emptySound;

    //public Transform projSpawnLocation;
    
    public static PlayerWeaponController Instance;
    
    private int[] weaponAmmo = {5, 5, 5};
    private Instrument currentInstrument = Instrument.Flute;
    private bool changingWeapon = false;
    private float nextFire = 0f;
    private float targetRot = 0;
    private int audioProgression = 0;

    private void Awake()
    {
        Instance = this;
    }
    
    void Update()
    {
        foreach (GameObject obj in weaponArray)
        {
            obj.SetActive(obj == weaponArray[currentInstrument.ToInt()]);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            changingWeapon = true;
            audioProgression = 0;
            targetRot += 120 % 360;
            currentInstrument = GetWeapon(1);
            nextFire = Time.time + 0.2f;
        }

        if (changingWeapon)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.x,rot.y,targetRot);
            var targetRotation = Quaternion.Euler(rot);
            
            var normalisedZRoation = transform.localEulerAngles.z;
            if (normalisedZRoation < 0)
                normalisedZRoation += 360.0f;
            if (normalisedZRoation < targetRot - 10)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, 
                    targetRotation, Time.deltaTime * weaponChangeSpeed);
            }
            else
            {
                weaponArray[GetWeapon(-1).ToInt()].SetActive(false);
                changingWeapon = false;
                nextFire = Time.time;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (weaponAmmo[currentInstrument.ToInt()] > 0 || currentInstrument == Instrument.Flute)
            {
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit) && Time.time >= nextFire)
                {
                    --weaponAmmo[currentInstrument.ToInt()];
                    var audioTracks = weaponArray[currentInstrument.ToInt()].GetComponents<AudioSource>();
                    audioTracks[audioProgression].Play();
                    audioProgression = (audioProgression + 1) % audioTracks.Length;
                    GameObject proj = Instantiate(projectilePrefabs[currentInstrument.ToInt()], projSpawnLocations[0].position, new Quaternion());
                    proj.transform.LookAt(hit.point);
                    proj.transform.Rotate(90f, 0f, 0f, Space.Self);

                    proj.gameObject.GetComponent<TimeClick>().criticalClickValue = SoundScheduler.Instance.Critical;
                    nextFire = Time.time + fireRates[currentInstrument.ToInt()];
                }
            }
            else
            {
                SoundGuy.Instance.PlaySound(Vector3.zero, 1, emptySound);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            
        }
    }

    Instrument GetWeapon(int offset)
    {
        var rawValue = currentInstrument.ToInt() + offset;
        if (rawValue < 0)
        {
            rawValue += 3;
        }
        return (Instrument) (rawValue % 3);
    }

    public void AddAmmo(Instrument instrument)
    {
        ++weaponAmmo[instrument.ToInt()];
    }
}
