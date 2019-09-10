using UnityEngine;

public class BackToMain : MonoBehaviour
{
    [SerializeField]
    private SceneFader sceneFader;

    public void LoadScene()
    {
        Time.timeScale = 1;
        sceneFader.FadeTo("LevelSelector");
    }
}
