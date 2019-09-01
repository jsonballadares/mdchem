using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class OptionsScreenController : MonoBehaviour
{
    public Slider music_slider;
    public Slider sfx_slider;
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
}
