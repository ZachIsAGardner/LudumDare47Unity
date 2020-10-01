using System.Collections.Generic;
using UnityEngine;

public class Song : SingleInstance<Song> 
{
    public List<AudioClip> Songs;

    private AudioSource audioSource;

    /// <summary>
    /// Start playing a song.
    /// </summary>
    /// <param name="songName">The name of the song to play.</param>
    public static void Play(string songName)
    {
        AudioClip clip = Instance.Songs.Find(f => f.name == songName);

        if (clip == null) return;

        if (Instance.audioSource == null) 
        {
            GameObject go = Game.New(clip.name);
            go.AddComponent<AudioSource>();
            Instance.audioSource = go.GetComponent<AudioSource>();
        }

        Instance.audioSource.PlayOneShot(clip);
    }
}