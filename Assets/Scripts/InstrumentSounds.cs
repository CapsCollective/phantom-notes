using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentSounds : MonoBehaviour
{
    public AudioClip[] floatingSounds;
    public AudioClip[] deathSounds;

    public AudioClip[] GetFloatingSounds()
    {
        return floatingSounds;
    }
    
    public AudioClip[] GetDeathSounds()
    {
        return deathSounds;
    }
}
