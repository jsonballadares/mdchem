using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPanelChildRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int amountOfSlots = 5;
        int amountOfChildren = gameObject.transform.childCount;
        List<int> list = new List<int>();
        for (int i = 0; i < amountOfSlots; i++)
        {
            int randNum = Random.Range(0, amountOfChildren);
            while (list.Contains(randNum))
            {
                randNum = Random.Range(0, amountOfChildren);
            }
            list.Add(randNum);
        }
        foreach (int x in list)
        {
            Debug.Log("NUMBER IN LIST --> " + x);
            gameObject.transform.GetChild(x).gameObject.SetActive(false);

        }

        Transform[] t = new Transform[gameObject.transform.childCount];
        for (int j = 0; j < gameObject.transform.childCount; j++)
        {
            t[j] = gameObject.transform.GetChild(j);
        }
        foreach (Transform x in t)
        {
            if (!x.gameObject.active)
            {
                x.SetParent(null);
            }
        }
    }

}
