using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {

   public static float timeThreshhold = 8.0f;
   void Update()
   {
       StartCoroutine(TimedThreshHold());
        
       if (timeThreshhold <= 0)
       {
           timeThreshhold += 10.0f;
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           //FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
       }

   }
   IEnumerator TimedThreshHold()
   {
       while (true)
       {
           yield return new WaitForSeconds(timeThreshhold);
           timeThreshhold--;
       }

   }
}
