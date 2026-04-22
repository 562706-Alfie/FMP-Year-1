using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    
    public float musicVolume, sfxVolume;
    public static AudioManager instance;
    public Sound[] sound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            // if instance is null, store a reference to this instance
            instance = this;
            DontDestroyOnLoad(gameObject);
            print("do not destroy");
        }
        else
        {
            // Another instance of this gameobject has been made so destroy it
            // as we already have one
            print("do destroy");
            Destroy(gameObject);
            return;
        }
        
        foreach (Sound s in sound)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.outputAudioMixerGroup = s.audioMixerGroup;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("musicVolume", 0f);
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("sfxVolume", 0f);
        }


    }

    // Update is called once per frame
    public void Play (string name)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        s.source.Play();
    }

    public void ChangeAudioSourceVolume(string name, float vol)
    {
        Sound s = Array.Find(sound, AudioSystem => AudioSystem.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Not found!");
            return;
        }
        s.source.volume = vol;


    }

    /*
    To get sound working:
    Add new sound in inspector in "audio manager" script on audio manager singleton
    Create new method in "buton script" which plays that sound
    Play sound via event trigger 

    To get audio mixer working:
    https://discussions.unity.com/t/setting-a-specific-audio-mixer-group-through-code/220611 ben grange

    You will need to have the audio play through the audio mixer
    https://www.youtube.com/watch?v=C1gCOoDU29M


    If you are using this in the future, everything should just work. Ensure the name is the same in buttonscript, an Audio mixer is selected on both the audio source on the AudioManager, and the sound in the array, and that the volume and pitch are at 1.
    */
}
