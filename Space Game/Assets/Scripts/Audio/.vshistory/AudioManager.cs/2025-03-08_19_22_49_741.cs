using UnityEngine;
using UnityEditor.Audio;
using System;
using System.Collections.Generic;


public class AudioManager : MonoBehaviour
{
    public Sound[] AmbiantSounds;
    private AudioSource AmbiantAudio;
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        AmbiantAudio = gameObject.AddComponent<AudioSource>();
        PlayAmbiant();
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void PlayAmbiant()
    {
        Sound s = AmbiantSounds[UnityEngine.Random.Range(0, AmbiantSounds.Length)];
        AmbiantAudio.clip = s.clip;
        AmbiantAudio.volume = s.volume;
        AmbiantAudio.pitch = s.pitch;

        s.source = AmbiantAudio;
        s.source.Play();
    }
}