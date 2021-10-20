using UnityEngine;
using UnityEngine.EventSystems;

using Manager;

public class SlotHandler : MonoBehaviour, IDropHandler
{
	public string lookingFor;

	public int lookingCount = 1;
	private int _currentCount = 0;

	public bool completed = false;

	public GameObject token = null;

	public AudioClip wrongAudioClip;

	public void OnDrop(PointerEventData eventData)
	{
		if (this.completed)
			return;

		if (eventData.pointerDrag != null)
		{
			if(MatchManager.instance != null)
				eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;

			string letter = eventData.pointerDrag.GetComponent<SlotToken>().myLetter;
			Debug.Log(letter);

			// tokens match the lookingFor string.
			if (string.Equals(lookingFor, letter))
            {
				this._currentCount++;
				if(this._currentCount == this.lookingCount)
					this.completed = true;

				this.token = eventData.pointerDrag;
				this.token.GetComponent<SlotToken>().draggable = false;

				if (MatchManager.instance != null)
				{
					MatchManager.instance.completedCount++;
					MatchManager.instance.CheckMatches();
				} 
				else if(DraggingManager.instance != null)
                {
					DraggingManager.instance.CheckMatches();
                } 
				else if(PuzzleManager.instance != null)
                {
					this.HideSlotAndToken();
					PuzzleManager.instance.CheckMatches();
                }
            } 
			else
            {
				// move the draggable word out of the slot.
				eventData.pointerDrag.GetComponent<SlotToken>().MoveBack();
				if (AudioManager.instance != null && this.wrongAudioClip != null)
					AudioManager.instance.PlaySoundEffect(this.wrongAudioClip);
				else if(this.wrongAudioClip != null)
					Utils.Utility.PlayOneShot(this.wrongAudioClip);
			}
		}
	}

	private void HideSlotAndToken()
    {
		if (this.token != null)
			this.token.SetActive(false);

		this.gameObject.SetActive(false);
    }
}
