using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    public GameObject progressButton;
    public Transform dropPanel;
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(gameObject.tag);

        if (!item && DraggableManager.count == 1 && Draggable.itemBeingDragged.tag.Equals("+1"))
        {
            if (transform.tag.Equals("+1"))
            {
                Draggable.itemBeingDragged.transform.SetParent(transform);

                checkSlots();
            }
        }
        else if (!item && DraggableManager.count == 2 && Draggable.itemBeingDragged.tag.Equals("+2"))
        {
            if (transform.tag.Equals("+2"))
            {
                Draggable.itemBeingDragged.transform.SetParent(transform);

                checkSlots();
            }
        }
        else if (!item && DraggableManager.count == 1 && Draggable.itemBeingDragged.tag.Equals("-1"))
        {
            if (transform.tag.Equals("-1"))
            {
                Draggable.itemBeingDragged.transform.SetParent(transform);

                checkSlots();
            }
        }
        else if (!item && DraggableManager.count == 2 && Draggable.itemBeingDragged.tag.Equals("-2"))
        {
            if (transform.tag.Equals("-2"))
            {
                Draggable.itemBeingDragged.transform.SetParent(transform);

                checkSlots();
            }
        }
        else if (!item && DraggableManager.count == 3 && Draggable.itemBeingDragged.tag.Equals("-3"))
        {
            if (transform.tag.Equals("-3"))
            {
                Draggable.itemBeingDragged.transform.SetParent(transform);

                checkSlots();
            }
        }
        else if (!item && DraggableManager.count == 1 && Draggable.itemBeingDragged.tag.Equals(gameObject.tag))
        {
            Draggable.itemBeingDragged.transform.SetParent(transform);

            checkSlots();
        }
        else if (!item && DraggableManager.count == 2 && Draggable.itemBeingDragged.tag.Equals(gameObject.tag))
        {
            Draggable.itemBeingDragged.transform.SetParent(transform);

            checkSlots();
        }
        else if (Draggable.itemBeingDragged.tag.Equals(gameObject.tag) && (SceneManager.GetActiveScene().name.Equals("Level10")||SceneManager.GetActiveScene().name.Equals("Level11")) )
        {
            Draggable.itemBeingDragged.transform.SetParent(transform);

            if(dropPanel.childCount >= 2){
                progressButton.SetActive(true);
            }else{
                progressButton.SetActive(false);
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().name.Equals("Level10") || SceneManager.GetActiveScene().name.Equals("Level11"))
            {

            }
            else
            {
                FindObjectOfType<AudioManager>().Play("falsenoise");
            }

        }
    }
    #endregion

    public void checkSlots()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level10") || SceneManager.GetActiveScene().name.Equals("Level11"))
        {

        }
        else
        {
            FindObjectOfType<AudioManager>().Play("truenoise");
        }

        int count = 0;
        for (int i = 0; i < dropPanel.childCount; i++)
        {
            if (dropPanel.GetChild(i).childCount > 0)
            {
                count++;
            }
        }

        if (SceneManager.GetActiveScene().name.Equals("Level6"))
        {
            if (count == 5)
            {
                Debug.Log(DraggableManager.count + " THE COUNT");
                if (DraggableManager.count == 2)
                {



                    progressButton.SetActive(true);

                }
                else
                {
                    progressButton.SetActive(true);
                }

            }
            else
            {
                Debug.Log("not 5");
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level7"))
        {
            if (DraggableManager.count == 1)
            {
                if (count == 4)
                {
                    progressButton.SetActive(true);
                }
            }
            else if (DraggableManager.count == 2)
            {
                if (count == 3)
                {
                    progressButton.SetActive(true);
                }
            }
            else if (DraggableManager.count == 3)
            {
                Debug.Log("test");
                if (count == 2)
                {
                    progressButton.SetActive(true);
                }
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level9"))
        {
            if (count == 5)
            {
                if (DraggableManager.count == 2)
                {
                    progressButton.SetActive(true);

                }
                else
                {
                    progressButton.SetActive(true);
                }

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level10"))
        {


        }
    }

}


