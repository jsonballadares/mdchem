using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class options_menu : MonoBehaviour
{
    public Slider music_slider;

    public Slider sfx_slider;

    public Image musicImage;
    public Image sfxImage;

    public Sprite soundOn;
    public Sprite soundOff;

    /*
    TODO 
    save user changes to player pref and load them in so they dont have to constantly change their settings
     */

    void Awake()
    {
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetFloat("music", music_slider.value);
        }
        else
        {
            music_slider.value = PlayerPrefs.GetFloat("music");
        }

        if (!PlayerPrefs.HasKey("sfx"))
        {
            PlayerPrefs.SetFloat("sfx", sfx_slider.value);
        }
        else
        {
            sfx_slider.value = PlayerPrefs.GetFloat("sfx");
        }
    }

    public void changeMusicIconOFF()
    {
        if (musicImage.sprite == soundOff)
        {
            musicImage.sprite = soundOn;
            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                FindObjectOfType<AudioManager>().changeAudioLevelMusic(music_slider.value);
            }
        }
        else
        {
            musicImage.sprite = soundOff;
            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                FindObjectOfType<AudioManager>().changeAudioLevelMusic(0f);
                music_slider.value = 0f;
            }
        }
    }

    public void sfxMusicIconOFF()
    {

        if (sfxImage.sprite == soundOff)
        {
            sfxImage.sprite = soundOn;
            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                FindObjectOfType<AudioManager>().changeAudioLevelSFX(sfx_slider.value);
                
            }
        }
        else
        {
            sfxImage.sprite = soundOff;
            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                FindObjectOfType<AudioManager>().changeAudioLevelSFX(0f);
                sfx_slider.value = 0f;
            }
        }

    }

    public void Update_music_volume()
    {

        if (GameObject.FindGameObjectWithTag("AudioManager") != null)
        {
            FindObjectOfType<AudioManager>().changeAudioLevelMusic(music_slider.value);
        }
    }

    public void Update_sfx_volume()
    {
        if (GameObject.FindGameObjectWithTag("AudioManager") != null)
        {
            FindObjectOfType<AudioManager>().changeAudioLevelSFX(sfx_slider.value);
        }
    }

    public void onExitOptions()
    {
        PlayerPrefs.SetFloat("music", music_slider.value);
        PlayerPrefs.SetFloat("sfx", sfx_slider.value);
    }
}
