using UnityEngine;

/*
This class is used to control the drag panel
 */
public class DragPanelController : MonoBehaviour
{
    private Transform dragPanel;

    void Start()
    {
        dragPanel = gameObject.transform;
        randomDragPanelOrder();
    }

    /*
    randomizes the order of children objects in the drag panels hiearchy
     */
    void randomDragPanelOrder()
    {
        for (int i = 0; i < dragPanel.childCount; i++)
            dragPanel.GetChild(i).SetSiblingIndex(Random.Range(0, dragPanel.childCount));
    }
}
