using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static void pauseGame()
    {
        Time.timeScale = 0;
    }
    public static void unPauseGame()
    {
        Time.timeScale = 1;
    }

}
