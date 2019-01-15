using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPanelController : MonoBehaviour
{
    private Transform dragPanel;

    void Start()
    {
        dragPanel = gameObject.transform;
        randomDragPanelOrder();
    }

    void randomDragPanelOrder()
    {

        for (int i = 0; i < dragPanel.childCount; i++)
        {
            dragPanel.GetChild(i).SetSiblingIndex(Random.Range(0, dragPanel.childCount));
        }

    }
}
