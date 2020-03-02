using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;

    public float camSpeedH, camSpeedV;
    public float acceleration;
    public float maxMoveSpeed;
    public float jumpForce;
    public float jumpClamp = 5;

    private float yaw, pitch;

    private GameObject bodyObj;
    private Camera cam;
    private GameObject camObj;
    private Rigidbody rb;
    private Vector3 movementForwardVector;
    private Vector3 movementSideVector;

    private bool grounded = true;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //bodyObj = transform.GetChild(0).gameObject;
        cam = Camera.main;
        camObj = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody>();

        movementSideVector = new Vector3(acceleration, 0, 0);
        movementForwardVector = new Vector3(0, 0, acceleration);
    }

    void Update()
    {
        //Rotate player and camera
        yaw += camSpeedH * Input.GetAxis("Mouse X");
        pitch += -1f * camSpeedV * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        camObj.transform.localEulerAngles = new Vector3(pitch, 0.0f, 0.0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, yaw, transform.eulerAngles.z);


        //Move player
        if (Input.GetKey(KeyCode.W))
        {
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

        Vector3 flatVelo = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        float yVelo = Mathf.Clamp(rb.velocity.y, float.MinValue, jumpClamp);
        rb.velocity = Vector3.ClampMagnitude(flatVelo, maxMoveSpeed);
        rb.velocity = new Vector3(rb.velocity.x, yVelo, rb.velocity.z);

        JumpCheck();
    }


    private void JumpCheck()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -Vector3.up);

        grounded = Physics.Raycast(ray, out hit, 1.5f);

        if (Input.GetKeyDown(KeyCode.Space) && grounded && rb.velocity.y >= -0.1f)
            rb.velocity += new Vector3(0, jumpForce, 0);
        
    }

}
