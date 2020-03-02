using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstrument : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float knockBack;
    
    private PlayerController player;
    private float time = 500;
    private float i = 0;
    private float rate = 0;

    private void Start()
    {
        player = PlayerController.Instance;
    }

    void Update()
    {
        var velocity = new Vector3();
        rate = 1/time;
        if (i < 1)
        {
            i += Time.deltaTime * rate;
            velocity = Vector3.Lerp(transform.position, player.transform.position, i);
            transform.position = velocity;
        }

        transform.LookAt(player.transform);
        
        if (Input.GetKeyUp(KeyCode.K))
        {
            GameObject newObj = Instantiate(pickupPrefab, transform.position, transform.rotation);
            Destroy(gameObject,0);
            newObj.GetComponent<Rigidbody>().velocity = velocity*knockBack;
        }
    }
}
