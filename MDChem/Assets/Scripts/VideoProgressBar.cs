using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoProgressBar : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    private Image progress;

    private void Awake()
    {
        progress = GetComponent<Image>();
    }

    private void Update()
    {
        if (videoPlayer.frameCount > 0)
            progress.fillAmount = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
    }

    public void OnDrag(PointerEventData eventData)
    {
        TrySkip(eventData);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TrySkip(eventData);

    }

    private void TrySkip(PointerEventData eventData)
    {
        videoPlayer.Pause();
        videoPlayer.Prepare();
        if (videoPlayer.isPrepared)
        {
            videoPlayer.Play();
        }
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            progress.rectTransform, eventData.position, null, out localPoint))
        {
            float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
            SkipToPercent(pct);
        }
        /*         videoPlayer.Pause();
                videoPlayer.Prepare();
                if (videoPlayer.isPrepared)
                {
                    videoPlayer.Play();
                } */
    }

    private void SkipToPercent(float pct)
    {
        videoPlayer.Pause();
        videoPlayer.Prepare();
        if (videoPlayer.isPrepared)
        {
            videoPlayer.Play();
        }
        var frame = videoPlayer.frameCount * pct;
        videoPlayer.frame = (long)frame;
        /*         videoPlayer.Pause();
                videoPlayer.Prepare();
                if (videoPlayer.isPrepared)
                {
                    videoPlayer.Play();
                } */
    }
}