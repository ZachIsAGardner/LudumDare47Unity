using System.Collections.Generic;
using UnityEngine;

public class Sound : SingleInstance<Sound>
{
    public List<AudioClip> Sounds;
    private static List<AudioSource> audioSources = new List<AudioSource>();

    void Update()
    {
        // Clean up audio sources that are done.
        for (int i = 0; i < audioSources.Count; i++)
        {
            var audioSource = audioSources[i];
            
            if (!audioSource.isPlaying) {
                audioSources.Remove(audioSource);
                Destroy(audioSource.gameObject);
            }
        }
    }

    /// <summary>
    /// Play a sound effect.
    /// </summary>
    /// <param name="sfxName">The name of the sfx to play.</param>
    public static void Play(string sfxName, bool interupt = true) 
    {
        AudioClip clip = Instance.Sounds.Find(f => f.name == sfxName);

        if (clip == null) return;

        AudioSource audioSource = (interupt) ? audioSources.Find(s => s.gameObject.name == clip.name) : null;

        if (audioSource == null) 
        {
            GameObject go = Game.New(clip.name);
            audioSource = go.AddComponent<AudioSource>();
            audioSources.Add(audioSource);
        }
        else 
        {
            audioSource.Stop();
        }

        audioSource.PlayOneShot(clip);
    }
}
