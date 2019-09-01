using UnityEngine;

public class LevelSelectorManager : MonoBehaviour
{
    /*
    Basically only used for managing the back button in the level selector
     */
    public void onBackButtonPress(){
        FindObjectOfType<AudioManager>().StopAllAudio();
        FindObjectOfType<AudioManager>().Play("buttonnoise");
        FindObjectOfType<SceneFader>().FadeTo("Home");
    }
    /*
    
     */
    /*
	Loads whatever level via the scene fader object
	 */
    public void Select(string levelName)
    {
        FindObjectOfType<AudioManager>().Play("buttonnoise");
        FindObjectOfType<SceneFader>().FadeTo(levelName);

    }
}
