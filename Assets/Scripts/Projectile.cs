using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed;
    public int secondsToDisappear;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Vector3 localVelo = new Vector3(0f, 0f, bulletSpeed);
        //rb.velocity = transform.InverseTransformVector(localVelo);

        rb.velocity = transform.up * bulletSpeed;
        Invoke(nameof(Disappear), secondsToDisappear);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<EnemyInstrument>().Damage(5);
        }
        Destroy(gameObject);
    }
    
    void Disappear()
    {
        Destroy(gameObject,0);
    }
}