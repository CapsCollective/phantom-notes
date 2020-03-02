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

    public List<SoundTrack> soundTracks;


    void Start()
    {
        StartCoroutine(PlayTrack(0));
    }

    private IEnumerator PlayTrack (int _trackID)
    {
        _trackID = _trackID % soundTracks.Count;

        Debug.Log("playing track: " + soundTracks[_trackID].track);

        yield return new WaitForSeconds(soundTracks[_trackID].trackLength);


        StartCoroutine(PlayTrack(_trackID+1));
    }

}
