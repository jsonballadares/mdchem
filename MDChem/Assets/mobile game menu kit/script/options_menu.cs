using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class options_menu : MonoBehaviour
{

    public OptionSoundElementSkin music_ico;
    public Slider music_slider;

    public OptionSoundElementSkin sfx_ico;
    public Slider sfx_slider;

    public OptionSoundElementSkin voice_ico;
    public Slider voice_slider;

    game_master my_game_master;

    // Use this for initialization
    void Start()
    {
    }

    public void Start_me()
    {

        if (game_master.game_master_obj)
        {
            my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");

            //music∂
            my_game_master.Music_on_off(game_master.music_on[my_game_master.current_profile_selected]);

            //sfx
            my_game_master.Sfx_on_off(game_master.sfx_on[my_game_master.current_profile_selected]);

            //voice
            my_game_master.Voice_on_off(game_master.voice_on[my_game_master.current_profile_selected]);

            Update_sound_icons();


        }
    }

    public void Music_on_off()
    {
        Debug.Log("MUSIC_ON_OFF CALLED");
        if (game_master.music_on[my_game_master.current_profile_selected])
        {
            my_game_master.Music_on_off(false);
            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                FindObjectOfType<AudioManager>().changeAudioLevelMusic(0f);
            }

        }
        else
        {
            my_game_master.Music_on_off(true);
            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                FindObjectOfType<AudioManager>().changeAudioLevelMusic(music_slider.value);
            }
        }

        Update_sound_icons();
        my_game_master.Gui_sfx(my_game_master.tap_sfx);
    }

    public void Sfx_on_off()
    {
        Debug.Log("SFX_ON_OFF CALLED");
        if (game_master.sfx_on[my_game_master.current_profile_selected])
        {
            my_game_master.Sfx_on_off(false);
            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                FindObjectOfType<AudioManager>().changeAudioLevelSFX(0f);
            }
        }
        else
        {
            my_game_master.Sfx_on_off(true);
            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                 FindObjectOfType<AudioManager>().changeAudioLevelSFX(sfx_slider.value);
            }
        }

        Update_sound_icons();
        my_game_master.Gui_sfx(my_game_master.tap_sfx);
    }

    public void Voice_on_off()
    {
        if (game_master.voice_on[my_game_master.current_profile_selected])
        {
            my_game_master.Voice_on_off(false);
        }
        else
        {
            my_game_master.Voice_on_off(true);
        }

        Update_sound_icons();
        my_game_master.Gui_sfx(my_game_master.tap_sfx);
    }

    void Update_sound_icons()
    {
        if (game_master.music_on[my_game_master.current_profile_selected])
        {
            music_ico.UpdateIcon(true);
            music_slider.interactable = true;
        }
        else
        {
            music_ico.UpdateIcon(false);
            music_slider.interactable = false;
        }

        if (game_master.sfx_on[my_game_master.current_profile_selected])
        {
            sfx_ico.UpdateIcon(true);
            sfx_slider.interactable = true;
        }
        else
        {
            sfx_ico.UpdateIcon(false);
            sfx_slider.interactable = false;
        }

        if (game_master.voice_on[my_game_master.current_profile_selected])
        {
            voice_ico.UpdateIcon(true);
            voice_slider.interactable = true;
        }
        else
        {
            voice_ico.UpdateIcon(false);
            voice_slider.interactable = false;
        }
    }

    public void Update_music_volume()
    {
        game_master.music_volume[my_game_master.current_profile_selected] = music_slider.value;
        if (GameObject.FindGameObjectWithTag("AudioManager") != null)
        {
            FindObjectOfType<AudioManager>().changeAudioLevelMusic(game_master.music_volume[my_game_master.current_profile_selected]);
        }
        my_game_master.Music_on_off(true);
    }

    public void Update_sfx_volume()
    {
        game_master.sfx_volume[my_game_master.current_profile_selected] = sfx_slider.value;
        if (GameObject.FindGameObjectWithTag("AudioManager") != null)
        {
            FindObjectOfType<AudioManager>().changeAudioLevelSFX(game_master.sfx_volume[my_game_master.current_profile_selected]);
        }
        my_game_master.Sfx_on_off(true);
    }

    public void Update_voice_volume()
    {
        game_master.voice_volume[my_game_master.current_profile_selected] = voice_slider.value;
        my_game_master.Voice_on_off(true);
    }
}
