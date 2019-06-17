using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Slots : MonoBehaviour, IDropHandler
{
    public DragManager dragManager;
    public GameObject item
    {
        get
        {
            if (!SceneManager.GetActiveScene().name.Equals("Level10"))
            {
                if (transform.childCount > 0)
                {
                    return transform.GetChild(0).gameObject;
                }
                return null;
            }
            else
            {
                return null;
            }

        }
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            Draggable.itemBeingDragged.transform.SetParent(transform);
        }
        if (SceneManager.GetActiveScene().name.Contains("Level1") || SceneManager.GetActiveScene().name.Contains("Level2") || SceneManager.GetActiveScene().name.Contains("Level3") || SceneManager.GetActiveScene().name.Contains("Level4") || SceneManager.GetActiveScene().name.Contains("Level6") || SceneManager.GetActiveScene().name.Contains("Level7") || SceneManager.GetActiveScene().name.Contains("Level9") || SceneManager.GetActiveScene().name.Equals("Level10"))
        {
            dragManager.checkDropPanel();
        }

    }
    #endregion

}