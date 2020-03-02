using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour
{

    public static BPM instance;

    public int bpm;

    public delegate void OnBeatDelegate();
    public static OnBeatDelegate beatDelegate;


    private bool beatEnabled;
    private int beatCounter;

    void Awake()
    {
        instance = this;
    }



    private void Update()
    {
        bool currentlyEnabled = Enabled;
        if (currentlyEnabled != beatEnabled)
        {
       //     beatDelegate();
            beatCounter++;
        }
        beatEnabled = currentlyEnabled;
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
