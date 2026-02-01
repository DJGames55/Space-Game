using UnityEngine;
using UnityEditor.Audio;

public class MusicLoader : MonoBehaviour
{
    public AudioClip[] ambiantMusic;

    private void Awake()
    {
        foreach (AudioClip music in ambiantMusic)
        {
            gameObject.AddComponent<AudioSource>();
        }
    }
}
