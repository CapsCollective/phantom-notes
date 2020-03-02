using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct SoundTrack
{
    public AudioClip track;
    public float trackLength;
}




public class SoundScheduler : MonoBehaviour
{



    public float trackLength, barsPerTrack = 4;
    public List<SoundTrack> soundTracks;

    private float timeCounter;

    private float visualFlashCounter = 0;

    public static SoundScheduler Instance;

    private float criticalMax;
    private float criticalCounter;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        StartCoroutine(PlayTrack(0));
    }

    private IEnumerator PlayTrack (int _trackID)
    {
        if (_trackID >= soundTracks.Count)
            yield break;

        Debug.Log("playing track: " + soundTracks[_trackID].track);

        yield return new WaitForSeconds(soundTracks[_trackID].trackLength);


        StartCoroutine(PlayTrack(_trackID+1));
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter > trackLength)
        {
            StartCoroutine(PlayTrack(0));
            timeCounter = 0;
        }


        criticalCounter += Time.deltaTime;
        criticalMax = (trackLength / soundTracks.Count) / barsPerTrack;
        if (criticalCounter > criticalMax)
        {
            criticalCounter = 0;
        }

    }

    public float Critical //returns a value 0-1 (where 1 is a stronger critical)
    {
        get
        {
            return 1 - (criticalCounter / criticalMax);

        }


    }


}
