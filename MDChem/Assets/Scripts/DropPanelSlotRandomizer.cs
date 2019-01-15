using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPanelSlotRandomizer : MonoBehaviour
{
    private Transform dropPanel;

    void Start()
    {
        dropPanel = gameObject.transform;
        randomDropPanelOrder();
    }
    void randomDropPanelOrder()
    {

        for (int i = 0; i < dropPanel.childCount; i++)
        {
            dropPanel.GetChild(i).SetSiblingIndex(Random.Range(0, dropPanel.childCount));
        }

    }

}
