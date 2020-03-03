using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed;

    public Instrument instrument;

    private Rigidbody rb;

    private int[] weaponDamages = {5, 3, 15};
    private float cullTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * bulletSpeed;
        if (instrument == Instrument.Tuba)
        {
            cullTime = Time.time + 3f;
        }
    }

    private void Update()
    {
        if (instrument == Instrument.Tuba)
        {
            if (Time.time >= cullTime) { Destroy(gameObject); }
            rb.transform.localScale += new Vector3(3 * Time.deltaTime, 0f, 3 * Time.deltaTime);
        }
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