using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
	
	private RectTransform recTrans;
	public CanvasGroup myCanvasGroup;
	public Canvas myCanvas;
	
	public enum letter {
		a,
		b,
		c
	};
	
	public letter myLetter;
	
	private void Start(){
		recTrans = this.GetComponent<RectTransform>();
		myCanvasGroup = this.GetComponent<CanvasGroup>();
	}
	
	public void OnBeginDrag(PointerEventData eventData) {
		//Debug.Log("BeginDrag");
		myCanvasGroup.blocksRaycasts = false;
	}
	
	public void OnDrag(PointerEventData eventData) {
		//Debug.Log("OnDrag");
		recTrans.anchoredPosition += eventData.delta / myCanvas.scaleFactor;
	}
	
	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log("EndDrag");
		myCanvasGroup.blocksRaycasts = true;
	}
	
	public void OnPointerDown(PointerEventData eventData) {
		Debug.Log("Down");
	}
}
