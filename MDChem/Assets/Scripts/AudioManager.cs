using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

/*
Controls the playing of audio throughout the game
 */
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager insance;

    private AudioSource[] allAudioSources;


    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (insance == null)
        {
            insance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
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
