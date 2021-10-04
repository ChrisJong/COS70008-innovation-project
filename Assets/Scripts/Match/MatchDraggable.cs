using UnityEngine;
using UnityEngine.EventSystems;

using Manager;

public class MatchDraggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool draggable = true;

    public Vector3 previousPosition = Vector3.zero;

    public string myLetter;

    [SerializeField] private RectTransform rectTransform;
    
    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake()
    {
        this.rectTransform = this.GetComponent<RectTransform>();
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!this.draggable)
            return;

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
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
    }

    public void MoveBack()
    {
        this.rectTransform.anchoredPosition3D = this.previousPosition;
    }
}
