using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, bulletSpeed, 0f);
    }

}