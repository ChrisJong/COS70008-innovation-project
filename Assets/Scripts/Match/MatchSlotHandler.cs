using UnityEngine;
using UnityEngine.EventSystems;

using Manager;

public class MatchSlotHandler : MonoBehaviour, IDropHandler
{
	public string lookingFor;

	public bool completed = false;

	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerDrag != null)
		{
			if(MatchManager.instance != null)
				eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;

			string letter = eventData.pointerDrag.GetComponent<MatchDraggable>().myLetter;
			Debug.Log(letter);
			if (string.Equals(lookingFor, letter))
            {
				this.completed = true;
				eventData.pointerDrag.GetComponent<MatchDraggable>().draggable = false;


				if (MatchManager.instance != null)
				{
					MatchManager.instance.completedCount++;
					MatchManager.instance.CheckMatches();
				}
            } 
			else
            {
				// move the draggable word out of the slot.
				eventData.pointerDrag.GetComponent<MatchDraggable>().MoveBack();
			}
		}
	}
}
