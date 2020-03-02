using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour
{

    public static BPM instance;

    public int bpm;


    public int beatCounter = 0;
    private bool a = true;

    void Awake()
    {
        instance = this;
    }

    public bool Enabled 
    {
        get
        {
            return ((Time.time / 60) * bpm % 1) >= 0.5f;
        }
    }

    public float BeatProgress // 0 to 1
    {
        get
        {
            return ((Time.time / 60) * bpm % 1);
        }
    }
    public float BeatProgressHalf // 0 to 1
    {
        get
        {
            return ((Time.time / 60) * (bpm/2) % 1);
        }
    }

}
