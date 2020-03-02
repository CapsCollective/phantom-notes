using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstrument : MonoBehaviour
{
    public GameObject player;
    private float time = 500;
    private float i = 0;
    private float rate = 0;

    // Update is called once per frame
    void Update()
    {
        rate = 1/time;
        if (i < 1)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(transform.position, player.transform.position, i);
        }

        transform.LookAt(player.transform);
    }
}
