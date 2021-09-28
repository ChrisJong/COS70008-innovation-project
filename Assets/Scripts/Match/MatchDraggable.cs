using UnityEngine;
using UnityEngine.EventSystems;

using Manager;

public class MatchDraggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public string myLetter;

    private RectTransform rectTransform;
    
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        this.rectTransform = this.GetComponent<RectTransform>();
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition += eventData.delta / MatchManager.instance.mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
    }
}
