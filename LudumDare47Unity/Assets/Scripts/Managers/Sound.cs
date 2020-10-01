using System.Collections.Generic;
using UnityEngine;

public class Sound : SingleInstance<Sound>
{
    public List<AudioClip> Sounds;

    /// <summary>
    /// Play a sound effect.
    /// </summary>
    /// <param name="sfxName">The name of the sfx to play.</param>
    public static void Play(string sfxName) 
    {

    }
}
