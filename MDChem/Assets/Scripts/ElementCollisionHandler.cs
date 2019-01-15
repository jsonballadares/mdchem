using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementCollisionHandler : MonoBehaviour, IBeginDragHandler
{
    private DragHandler drag;
    private Collider2D col;
    private Image image;
    private DateTime beginInteraction;
    private DateTime endInteraction;

    private Rigidbody2D rb;


    void Awake()
    {
        Physics2D.IgnoreLayerCollision(10, 10);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        image = GetComponent<Image>();
        drag = GetComponent<DragHandler>();
        col = GetComponent<Collider2D>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        rb.Sleep();
        var box = other.gameObject;
        var element = gameObject;

        if (CorrectAnswer(box))
        {
            
            image.color = Color.green;
            Die();

            endInteraction = DateTime.Now;
            TimeSpan duration = endInteraction.Subtract(beginInteraction);
            Debug.Log("the duration is " + duration.Milliseconds);
            if (duration.Milliseconds <= 200)
            {
                Score.scoreValue += 20;
                FindObjectOfType<AudioManager>().Play("bonusnoise");
            }
            else
            {
                Score.scoreValue += 10;
                FindObjectOfType<AudioManager>().Play("correctnoise");
            }

            Element e = new Element(element.name.Replace("(Clone)",""), Math.Abs(duration.Milliseconds), Element.State.Correct);
            Element.elementArray.Add(e);
            Debug.Log("element name --> " + e.getName() + " element duration --> " + e.getDuration() + " element state --> " + e.getState());
        }
        else if (box.CompareTag("BottomBoxCollider"))
        {
            TimeSpan duration = endInteraction.Subtract(beginInteraction);
            Element e = new Element(element.name.Replace("(Clone)",""), Math.Abs(duration.Milliseconds), Element.State.Missed);
            Element.elementArray.Add(e);
            Debug.Log("element name --> " + e.getName() + " element duration --> " + e.getDuration() + " element state --> " + e.getState());
            Destroy(element);
            FindObjectOfType<AudioManager>().Play("destroy");

        }
        else
        {
            TimeSpan duration = endInteraction.Subtract(beginInteraction);
            Element e = new Element(element.name.Replace("(Clone)",""), Math.Abs(duration.Milliseconds), Element.State.Incorrect);
            Element.elementArray.Add(e);
            Debug.Log("element name --> " + e.getName() + " element duration --> " + e.getDuration() + " element state --> " + e.getState());
            FindObjectOfType<AudioManager>().Play("wrongnoise");
            image.color = Color.red;
            Die();
            Score.scoreValue -= 10;
            
        }



    }

    void Die()
    {
        col.enabled = false;
        drag.enabled = false;
        StartCoroutine("FadeOut");
    }

    bool CorrectAnswer(GameObject box)
    {
        var element = gameObject;

        return (box.CompareTag("LeftBox") && element.CompareTag("LeftElement")) ||
            (box.CompareTag("RightBox") && element.CompareTag("RightElement"));
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color color = image.color;
            color.a = f;
            image.color = color;
            yield return new WaitForSeconds(.05f);
        }
        Destroy(gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginInteraction = DateTime.Now;
    }
}
