using UnityEngine;
using UnityEditor.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System.Linq;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private UIManager _ui;

    public AudioMixerGroup Music;
    public AudioMixerGroup SFXMixer;

    public Sound[] AmbiantSounds;
    private AudioSource AmbiantAudio;
    public Sound[] sfx;
    private Sound[] sceneSounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);


        foreach (Sound s in sfx)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = SFXMixer;
        }

        AmbiantAudio = gameObject.AddComponent<AudioSource>();
        AmbiantAudio.outputAudioMixerGroup = Music;
        PlayAmbiant();
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sfx, sound => sound.name == name);
        s.source.Play();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sfx, sound => sound.name == name);
        s.source.Stop();
    }

    public void PlayAmbiant()
    {
        Sound s = AmbiantSounds[UnityEngine.Random.Range(0, AmbiantSounds.Length)];

        foreach (Sound sound in AmbiantSounds)
        {
            if (sound.musicAtributes.onlySpecificScenes)
                foreach (SceneField scene in sound.musicAtributes.specificScenes)
                    if (scene.SceneName == _ui.currentScene.SceneName)
                        if (sound.musicAtributes.prioritySound)
                            sceneSounds.Append(sound);
        }

        while (s.musicAtributes.onlySpecificScenes)
        {
            foreach (SceneField scene in s.musicAtributes.specificScenes)
            {
                if (scene.SceneName == _ui.currentScene.SceneName)
                    break;
            }

            s = AmbiantSounds[UnityEngine.Random.Range(0, AmbiantSounds.Length)];
        }

        s = sceneSounds[UnityEngine.Random.Range(0, sceneSounds.Length)];

        AmbiantAudio.clip = s.clip;
        AmbiantAudio.volume = s.volume;
        AmbiantAudio.pitch = s.pitch;

        s.source = AmbiantAudio;
        s.source.Play();
        StartCoroutine(MusicFinished());
    }

    IEnumerator MusicFinished()
    {
        yield return new WaitWhile(() => AmbiantAudio.isPlaying);
        PlayAmbiant();
    }
}