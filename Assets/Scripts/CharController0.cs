using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// credit https://gamedev.stackexchange.com/questions/104693/how-to-use-input-getaxismouse-x-y-to-rotate-the-camera


public class CharController0 : MonoBehaviour
{

    public float speedH, speedV;
    public float acceleration;
    public float maxMoveSpeed;

    private float yaw, pitch;

    private GameObject bodyObj;
    private Camera cam;
    private GameObject camObj;
    private Rigidbody rb;
    private Vector3 movementForwardVector;
    private Vector3 movementSideVector;

    void Start()
    {
        //bodyObj = transform.GetChild(0).gameObject;
        cam = Camera.main;
        camObj = transform.GetChild(0).gameObject;
        print(cam);
        rb = GetComponent<Rigidbody>();

        movementSideVector = new Vector3(acceleration, 0, 0);
        movementForwardVector = new Vector3(0, 0, acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate player and camera
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch += -1f * speedV * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        camObj.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, yaw, transform.eulerAngles.z);


        //Move player
        if (Input.GetKey(KeyCode.W)) {
            rb.AddRelativeForce(movementForwardVector, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(-movementForwardVector, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(movementSideVector, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(-movementSideVector, ForceMode.Acceleration);
        }

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxMoveSpeed);
    }
}
