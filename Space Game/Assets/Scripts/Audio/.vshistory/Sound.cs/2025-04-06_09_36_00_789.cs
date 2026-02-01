using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name = "Audio Clip";
    public AudioClip clip;

    [Range(0f, 1f)] public float volume = 1;
    [Range(.1f, 3f)] public float pitch = 1;

    [Header("Scenes")]
    public bool onlySpecificScenes;
    public SceneField[] specificScenes;
    public bool prioritySound;

    [HideInInspector] public AudioSource source;
}