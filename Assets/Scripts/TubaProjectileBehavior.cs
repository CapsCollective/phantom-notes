using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubaProjectileBehavior : MonoBehaviour
{
    public float forwardSpeed;
    public float expansionSpeed;
    public float rotationSpeed;
    public float lifeDuration = 4f;

    private Rigidbody rb;
    private float yRot = 0f;
    private float scale = 1f;
    //private Vector3 initialScale;
    private Vector3 currentScale;
    private float cullTime;
    public TimeClick timeClickObj;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * forwardSpeed;
        currentScale = rb.transform.localScale;
        cullTime = Time.time + lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= cullTime)
        {
            Destroy(gameObject);
        }

        yRot += rotationSpeed * Time.deltaTime;
        //currentScale += new Vector3(expansionSpeed * Time.deltaTime, expansionSpeed * Time.deltaTime, expansionSpeed * Time.deltaTime);
        currentScale += new Vector3(expansionSpeed * Time.deltaTime, 0f, expansionSpeed * Time.deltaTime);

        //rb.transform.rotation = Quaternion.Euler(rb.transform.rotation.x, yRot, rb.transform.rotation.z);
        //rb.transform.Rotate(transform.up, rotationSpeed * Time.deltaTime, Space.Self);
        rb.transform.RotateAround(transform.position, transform.up, rotationSpeed * Time.deltaTime);
        rb.transform.localScale = currentScale;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<EnemyInstrument>().Damage(15, timeClickObj);
        }
        else if (collision.gameObject.CompareTag("Untagged"))
        {
            Destroy(gameObject);
        }
    }
}
