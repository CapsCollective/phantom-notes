using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
//    public GameObject flute;
//    public GameObject guitar;
//    public GameObject tuba;
//    public GameObject symbal;
//
//    public GameObject[] weaponArray;
//    private Transform[] transformArray;
//    private int[] ammoArray;
//
//    public GameObject rotationPoint;
//    public float weaponSwitchTime;
//    public GameObject activatePoint;
//    public GameObject deactivatePoint;
    public int weaponChangeSpeed;

    private Instrument currentWeapon;
    private Instrument nextWeapon;
    private bool changingWeapon = false;
    private float targetRot = 0;

    void Start()
    {
//        currentWeapon = Instrument.Flute;
//
//        weaponArray = new GameObject[] { flute, guitar, tuba, symbal };
//        transformArray = new Transform[] { flute.transform, guitar.transform, tuba.transform, symbal.transform };
//        ammoArray = new int[] { 3, 1, 1, 1 };
//        flute.SetActive(true);
//        guitar.SetActive(false);
//        tuba.SetActive(false);
//        symbal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
//        if (changingWeapon)
//        {
//            weaponArray[(int)currentWeapon].transform.RotateAround(rotationPoint.transform.position, transform.forward, weaponSwitchTime * Time.deltaTime);
//        }
//        //float deactivateTime;
//        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapon != Instrument.Flute)
//        {
//            flute.SetActive(true);
//            float deactivateTime = Time.time + weaponSwitchTime;
//        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            changingWeapon = true;
            targetRot += 120 % 360;
        }

        if (changingWeapon)
        {
            var targetQuat = Quaternion.Euler(0, 0, targetRot);
            var rotation = transform.rotation;
            if (rotation.z < targetRot - 1)
            {
                transform.rotation = Quaternion.Slerp(rotation, 
                    targetQuat, 
                    Time.deltaTime * weaponChangeSpeed);
            }
            else
            {
                changingWeapon = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            
        }
    }
}
