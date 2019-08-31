using UnityEngine;
using UnityEngine.EventSystems;

/*
This class will handle all the drag events and things 
that deal with draggging objects on the screen
 */
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    public Vector3 startPosition;
    public Transform startParent;

    /*
    When has just began dragging this is the code that is ran
     */
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject; /* set the static instance field to be whatever current object is being */
        startPosition = transform.position; /* keep track of the starting position so we can snap back if they drop it */
        startParent = transform.parent; /* we need this for the same reason as the starting position */
        GetComponent<CanvasGroup>().blocksRaycasts = false; /* if we block raycasts dropping elements in their boxes wont work */
        transform.SetParent(transform.root); /* needed for snapping */
    }

    /*
    When an object is dragged this event is called
     */
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; /* set the position of the element being dragged to the current position from a drag */
    }

    /*
    when a drag ends this is the event that is called
     */
    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null; /* now that the drag has ended clear the var */
        GetComponent<CanvasGroup>().blocksRaycasts = true; /* turn on raycasts because its what lets dragging work */
        /* this code handles the snapping if we essentially didnt drop it somewhere snap back to old position*/
        if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
