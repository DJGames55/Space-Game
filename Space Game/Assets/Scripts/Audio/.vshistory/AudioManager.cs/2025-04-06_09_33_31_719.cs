using UnityEngine;
using UnityEditor.Audio;
using System;
using System.Collections;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private UIManager _ui;

    public AudioMixerGroup Music;
    public AudioMixerGroup SFXMixer;

    public Sound[] AmbiantSounds;
    private AudioSource AmbiantAudio;
    public Sound[] sfx;

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

        while (s.onlySpecificScenes)
        {
            foreach (SceneField scene in s.specificScenes)
            {
                if scene.SceneName == 
            }
        }

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