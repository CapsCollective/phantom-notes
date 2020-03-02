using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGuy : MonoBehaviour
{
    public static SoundGuy Instance;
    public AudioSource audioObject;

    void Awake()
    {
        Instance = this;
    }


    public void PlaySound (float pitch, AudioClip audioFile)
    {
        StartCoroutine(PlaySoundRun(pitch, audioFile));

    }

    private IEnumerator PlaySoundRun (float pitch, AudioClip audioFile)
    {
        AudioSource newAudioObject = Instantiate(audioObject);
       // AudioClip clip = (AudioClip)Resources.Load("Sounds/" + filename, typeof(AudioClip));
        newAudioObject.clip = audioFile;
        newAudioObject.pitch = pitch;

        newAudioObject.Play();

        yield return new WaitForSeconds(audioFile.length);

        GameObject.Destroy(newAudioObject);
    }



}
