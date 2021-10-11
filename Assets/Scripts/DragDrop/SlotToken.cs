using UnityEngine;
using UnityEngine.EventSystems;

using Manager;

public class SlotToken : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public bool draggable = true;

    public Vector3 previousPosition = Vector3.zero;

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
        if (!this.draggable)
            return;

        this.transform.SetAsLastSibling();
        this.previousPosition = this.rectTransform.anchoredPosition3D;
        this.canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!this.draggable)
            return;

        if (MatchManager.instance != null)
            this.rectTransform.anchoredPosition += eventData.delta / MatchManager.instance.mainCanvas.scaleFactor;
        else
        {
            Vector3 temp = Camera.main.ScreenToWorldPoint(eventData.position);
            temp.z = 0.0f;
            transform.position = temp;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.canvasGroup.blocksRaycasts = true;

        if (PuzzleManager.instance != null)
        {
            if (eventData.pointerDrag.GetComponent<SlotHandler>() == null)
                this.MoveBack();
            else
                Debug.Log("Something");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<SlotToken>().MoveBack();
    }

    public void MoveBack()
    {
        this.rectTransform.anchoredPosition3D = this.previousPosition;
    }
}
