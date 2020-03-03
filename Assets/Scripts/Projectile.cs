﻿using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Vector3 localVelo = new Vector3(0f, 0f, bulletSpeed);
        //rb.velocity = transform.InverseTransformVector(localVelo);

        rb.velocity = transform.up * bulletSpeed;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<EnemyInstrument>().Damage(5);
        }
        Destroy(gameObject);
    }
}