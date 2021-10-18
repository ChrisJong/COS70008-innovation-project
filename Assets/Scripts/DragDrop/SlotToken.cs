using UnityEngine;
using UnityEngine.EventSystems;

using Manager;
using Utils;

public class SlotToken : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public bool draggable = true;

    public string myLetter;

    [Space(10), Header("Audio")]
    public AudioClip tokenAudioClip;

    private Vector3 _previousPosition = Vector3.zero;

    private RectTransform _rectTransform;
    
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        this._rectTransform = this.GetComponent<RectTransform>();
        this._canvasGroup = this.GetComponent<CanvasGroup>();

        this.draggable = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!this.draggable)
            return;

        if (AudioManager.instance != null & this.tokenAudioClip != null)
            AudioManager.instance.PlaySoundEffect(this.tokenAudioClip);
        else if(this.tokenAudioClip != null)
            Utility.PlayOneShot(this.tokenAudioClip);

        this.transform.SetAsLastSibling();
        this._previousPosition = this._rectTransform.anchoredPosition3D;
        this._canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!this.draggable)
            return;

        if (MatchManager.instance != null)
            this._rectTransform.anchoredPosition += eventData.delta / MatchManager.instance.mainCanvas.scaleFactor;
        else
        {
            Vector3 temp = Camera.main.ScreenToWorldPoint(eventData.position);
            temp.z = 0.0f;
            transform.position = temp;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!this.draggable)
            return;

        this._canvasGroup.blocksRaycasts = true;

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
        if (!this.draggable)
            return;

        eventData.pointerDrag.GetComponent<SlotToken>().MoveBack();
    }

    public void MoveBack()
    {
        this._rectTransform.anchoredPosition3D = this._previousPosition;
    }
}
