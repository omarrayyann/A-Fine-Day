using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    // Array storing all the sounds used in the game
    public Sound[] sounds;
    // Used to decide which theme to play upon entering a scene
    public string game;

    // Adding audio sources to all our sounds
    void Awake ()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // When the script starts, begin playing the background music of the given scene
    private void Start()
    {
        Play(game);
    }

    // Script to play any sound
    public void Play(string name)
    {
        // Searching in sounds such that the name of the sound is equal to the requested name
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null) // If nothing is found, print an error message and don't attempt to play the sound
        {
            Debug.Log("Error playing the sound");
            return;
        }
        s.source.Play();
    }
}
