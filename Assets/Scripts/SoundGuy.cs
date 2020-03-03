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


    public void PlaySound (Vector3 _pos, float pitch, AudioClip audioFile)
    {
        StartCoroutine(PlaySoundRun(_pos, pitch, audioFile));

    }
    public void PlaySound(Vector3 _pos, float pitch, AudioClip audioFile, bool _d)
    {
        if (_d)
        {
            DontDestroyOnLoad(gameObject);
        }
        StartCoroutine(PlaySoundRun(_pos, pitch, audioFile));
    }

    public void PlaySound(Vector3 _pos, float pitch, AudioClip audioFile, bool _d, bool _k)
    {
        if (_d)
        {
            DontDestroyOnLoad(gameObject);
        }
        StartCoroutine(PlaySoundRun(_pos, pitch, audioFile, _k));
    }

    private IEnumerator PlaySoundRun (Vector3 _pos, float pitch, AudioClip audioFile, bool _keepAlive = false)
    {
        AudioSource newAudioObject = Instantiate(audioObject);
        newAudioObject.transform.position = _pos;
       // AudioClip clip = (AudioClip)Resources.Load("Sounds/" + filename, typeof(AudioClip));
        newAudioObject.clip = audioFile;
        newAudioObject.pitch = pitch;
        if (_keepAlive)
            newAudioObject.loop = true;

        newAudioObject.Play();

        yield return new WaitForSeconds(audioFile.length);

        if (!_keepAlive)
            GameObject.Destroy(newAudioObject.gameObject);
    }



}
