using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public Camera cam;
    public GameObject projectilePrefab;
    public GameObject[] weaponArray;
    public int weaponChangeSpeed;

    public float projVDisplacement, projHDisplacement;
    public Transform projSpawnLocation;
    
    public static PlayerWeaponController Instance;

    private int[] weaponAmmo = {5, 1, 1, 1};
    private Instrument currentWeapon = Instrument.Flute;
    private bool changingWeapon = false;
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
            obj.SetActive(obj == weaponArray[(int) currentWeapon]);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            changingWeapon = true;
            audioProgression = 0;
            targetRot += 120 % 360;
            currentWeapon = GetWeapon(1);
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
                weaponArray[(int) GetWeapon(-1)].SetActive(false);
                changingWeapon = false;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (weaponAmmo[(int) currentWeapon] > 0)
            {
                --weaponAmmo[(int) currentWeapon];
                //var projectileInstance = Instantiate(projectile, transform.position, cam.transform.rotation);
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
                {
                    //raycast hits something
                    //print("aiming at: " + hit.collider.gameObject.name);
                    Vector3 spawnDisplacement = new Vector3(projHDisplacement, projVDisplacement, 0f);
                    spawnDisplacement = cam.transform.InverseTransformVector(spawnDisplacement);
                    //GameObject proj = Instantiate(projectilePrefab, cam.transform.position - spawnDisplacement, new Quaternion());
                    GameObject proj = Instantiate(projectilePrefab, projSpawnLocation.position, new Quaternion());
                    proj.transform.LookAt(hit.point);
                    proj.transform.Rotate(90f, 0f, 0f, Space.Self);
                }

                //projectileInstance.GetComponent<Rigidbody>().velocity = transform.InverseTransformPoint(Vector3.forward) * 0.1f;
                var sounds = weaponArray[(int) currentWeapon].GetComponents<AudioSource>();
                sounds[audioProgression].Play();
                audioProgression = (audioProgression + 1) % sounds.Length;
            }
            else
            {
                //play click
            }
        }
    }

    Instrument GetWeapon(int offset)
    {
        var rawValue = (int) currentWeapon + offset;
        if (rawValue < 0)
        {
            rawValue += 3;
        }
        return (Instrument) (rawValue % 3);
    }

    public void AddAmmo(Instrument instrument)
    {
        ++weaponAmmo[(int) instrument];
    }
}
