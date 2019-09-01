using UnityEngine;
using UnityEngine.SceneManagement;

public class MiscDialogHandler : MonoBehaviour
{
    public void onButtonPress()
    {
        if (SceneManager.GetActiveScene().name.Contains("Beginner"))
        {
            FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
        }
    }
}
