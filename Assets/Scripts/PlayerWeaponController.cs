using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject[] weaponArray;
    public int weaponChangeSpeed;

    private Instrument currentWeapon = Instrument.Flute;
    private bool changingWeapon = false;
    private float targetRot = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            changingWeapon = true;
            targetRot += 120 % 360;
            currentWeapon = GetWeapon(1);
            weaponArray[(int) currentWeapon].SetActive(true);
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
    }

    Instrument GetWeapon(int offset)
    {
        return (Instrument) (((int) currentWeapon + offset) % 3);
    }
}
