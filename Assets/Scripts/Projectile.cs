using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed;

    public Instrument instrument;

    private Rigidbody rb;

    private int[] weaponDamages = {5, 3, 15};

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * bulletSpeed;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<EnemyInstrument>().Damage(weaponDamages[(int) instrument]);
        }
        Destroy(gameObject);
    }
}