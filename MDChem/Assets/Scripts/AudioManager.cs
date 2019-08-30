using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

/*
Controls the playing of audio throughout the game. Using a single instance and the various methods
avaliable to this class allows us to play, pause, and adjust audio.
 */
public class AudioManager : MonoBehaviour
{
    //an array of Sound objects 
    public Sound[] sounds;

    //the singleton instance of the class 
    public static AudioManager insance;

    //the unity AudioSources we will be manipulating with the sound class
    private AudioSource[] allAudioSources;

    /*
    called before the start function is used in this case to initialize the singleton
    and keep it persistent through different scenes
     */
    void Awake()
    {
        /*
        if the field "instance" isnt initialized,
        initialize it to whatever gameobject this script is attached to
         */
        if (insance == null)
        {
            insance = this;
        }
        else
        {
            /*
            if the instance isnt null destroy the current gameobject 
            the script is attached to ensures there is only one audiomanager
             */
            Destroy(gameObject);
            return; //return to stop execution
        }
        /* now that we have only one singleton object of the audio manager */

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("music"))
        {
            this.changeAudioLevelMusic(PlayerPrefs.GetFloat("music"));
        }
        if (PlayerPrefs.HasKey("sfx"))
        {
            this.changeAudioLevelSFX(PlayerPrefs.GetFloat("sfx"));
        }

    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Pause();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    public void changeAudioLevelMusic(float level)
    {
        Debug.Log("CHANGING THE MUSIC AUDIO LEVEL");
        foreach (Sound s in sounds)
        {
            if (s.tag.Equals("music"))
            {
                s.volume = level;
                s.source.volume = level;
            }
        }
    }

    public void changeAudioLevelSFX(float level)
    {
        Debug.Log("CHANGING THE SFX AUDIO LEVEL");
        foreach (Sound s in sounds)
        {
            if (s.tag.Equals("sfx"))
            {
                s.volume = level;
                s.source.volume = level;
            }
        }
    }


}
