using UnityEngine.Audio;
using UnityEngine;
using System;
[System.Serializable]

public class AudioManager : MonoBehaviour
{

    public Sounds_Stuff[] sounds;
    public static AudioManager Instance;
    private void Awake()
    {
        foreach (Sounds_Stuff S in sounds)
        {
            S.source = gameObject.AddComponent<AudioSource>();
            S.source.clip = S.Clip;
            S.source.volume = S.Volume;
            S.source.loop = S.loop;


        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Play("StartMenu");
    }
    public void Play(string name)
    {
        Sounds_Stuff Sounds_Play = Array.Find(sounds, sounds => sounds.Name == name);

        Sounds_Play.source.Play();

    }




}


//Audio Stuff sliders volume pitch looping ETC

[System.Serializable]
public class Sounds_Stuff
{
    public string Name;

    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume;


    public bool loop;


    [HideInInspector]
    public AudioSource source;



}