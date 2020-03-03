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
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<EnemyInstrument>().Damage(5);
        }
    }
}