using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject flute;
    public GameObject guitar;
    public GameObject tuba;
    public GameObject symbal;

    public GameObject[] weaponArray;
    private Transform[] transformArray;
    private int[] ammoArray;

    public GameObject rotationPoint;
    public float weaponSwitchTime;
    public GameObject activatePoint;
    public GameObject deactivatePoint;

    private Instrument currentWeapon;
    private Instrument nextWeapon;
    private bool changingWeapon = false;

    void Start()
    {
        currentWeapon = Instrument.Flute;

        weaponArray = new GameObject[] { flute, guitar, tuba, symbal };
        transformArray = new Transform[] { flute.transform, guitar.transform, tuba.transform, symbal.transform };
        ammoArray = new int[] { 3, 1, 1, 1 };
        flute.SetActive(true);
        guitar.SetActive(false);
        tuba.SetActive(false);
        symbal.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (changingWeapon)
        {
            weaponArray[(int)currentWeapon].transform.RotateAround(rotationPoint.transform.position, transform.forward, weaponSwitchTime * Time.deltaTime);
        }
        //float deactivateTime;
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapon != Instrument.Flute)
        {
            flute.SetActive(true);
            float deactivateTime = Time.time + weaponSwitchTime;
        }
    }
}
