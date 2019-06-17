using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevelManager : MonoBehaviour
{
    public static LastLevelManager insance;

    public GameObject dropPanel,dragPanel;

    void Start()
    {
        if (insance == null)
        {
            insance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
