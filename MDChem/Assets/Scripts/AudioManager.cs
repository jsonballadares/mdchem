using UnityEngine;
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

        /* initializes the Sound objects in the array sounds */
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
        /*
        Checks if the playerprefs has the key called music/sfx
        if it does this mean it has been set to something besides default
        so we set it to whatever value it has been changed to by the user
         */

        if (PlayerPrefs.HasKey("music"))
        {
            this.changeAudioLevelMusic(PlayerPrefs.GetFloat("music"));
        }

        if (PlayerPrefs.HasKey("sfx"))
        {
            this.changeAudioLevelSFX(PlayerPrefs.GetFloat("sfx"));
        }
    }

    /*
    Plays whatever string is passed into the name parameter
     */
    public void Play(string name)
    {
        /* assign s to whatever is passed into the name parameter if it exists 
        in the sounds array and play it if not just break (s will be null) */

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    /*
    Pauses whatever string is passed into the name parameter 
     */
    public void Pause(string name)
    {
        /* assign s to whatever is passed into the name parameter if it exists 
        in the sounds and play it if not just break (s will be null) */

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Pause();
    }

    /*
    Stops whatever string is passed into the name parameter 
     */
    public void Stop(string name)
    {
        /* assign s to whatever is passed into the name parameter if it exists 
        in the sounds and play it if not just break (s will be null) */

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    /*
    Stops all audio from playing
     */
    public void StopAllAudio()
    {
        /*
        get all objects that can be stopped and iterate for each one and stop them
         */
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    /*
    Changes the audio level specifically for the music in the game to 
    the desired float parameter
     */
    public void changeAudioLevelMusic(float level)
    {
        /*
        iterate through every sound object in the sounds array 
        and if the tag for that object is music change its 
        sound level to match that of the parameter
         */
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

    /*
    Changes the audio level specifically for the SFX in the game to 
    the desired float parameter
     */
    public void changeAudioLevelSFX(float level)
    {
        /*
        iterate through every sound object in the sounds array 
        and if the tag for that object is music change its 
        sound level to match that of the parameter
        */
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
