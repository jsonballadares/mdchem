using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Rigidbody2D rb;
    public static int gravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        Physics2D.gravity = new Vector2(0, gravity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rb.gravityScale = 1;
    }
}
