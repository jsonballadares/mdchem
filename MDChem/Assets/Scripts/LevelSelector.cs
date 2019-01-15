using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    private SceneFader sceneFader;

    [SerializeField]
    private Button[] levelButtons;

    /*
    Initializes the scene and checks for the highest level that has been unlocked
     */

    void Start()
    {
        
        FindObjectOfType<AudioManager>().StopAllAudio();
        FindObjectOfType<AudioManager>().Play("levelselectreggae");
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }
    }

    /*
	Loads whatever level via the scene fader object
	 */
    public void Select(string levelName)
    {

        FindObjectOfType<AudioManager>().Play("buttonnoise");
        sceneFader.FadeTo(levelName);

    }
}
