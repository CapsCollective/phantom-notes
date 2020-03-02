using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public Camera cam;
    public Projectile projectile;
    public GameObject[] weaponArray;
    public int weaponChangeSpeed;

    private Instrument currentWeapon = Instrument.Flute;
    private bool changingWeapon = false;
    private float targetRot = 0;
    
    void Update()
    {
        foreach (GameObject obj in weaponArray)
        {
            obj.SetActive(obj == weaponArray[(int) currentWeapon]);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            changingWeapon = true;
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
            var projectileInstance = Instantiate(projectile, transform.position, cam.transform.rotation);
            projectileInstance.GetComponent<Rigidbody>().velocity = transform.InverseTransformPoint(Vector3.forward) * 0.1f;
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
}
