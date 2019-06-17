using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCanvasFader : MonoBehaviour
{
    public CanvasGroup uiElement;

    void Start()
    {
        if (!this.gameObject.tag.Equals("miscDialog"))
        {
            FadeIn();
        }

    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCanvasGroup(uiElement, uiElement.alpha, 1, .5f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCanvasGroup(uiElement, uiElement.alpha, 0, .5f));

    }

    private IEnumerator FadeInCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();
        }

        print("done");
    }

    private IEnumerator FadeOutCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();
        }

        gameObject.SetActive(false);
    }
}
