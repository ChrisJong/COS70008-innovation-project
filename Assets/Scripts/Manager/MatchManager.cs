namespace Manager
{
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using Extension;
    using Utils;

    public class MatchManager : SingletonMono<MatchManager>
    {
        public int completedCount = 0;

        public Canvas mainCanvas;

        [Space(10), Header("Editable")]
        public List<SlotHandler> slots;

        public GameObject boardOutline;
        public GameObject boardCompleted;

        [Space(10), Header("Audio")]
        public AudioClip slotCompleteAudioClip;
        public AudioClip SuccessAudioClip;

        public void CheckMatches()
        {
            if (this.completedCount == this.slots.Count)
            {
                onActivityComplete();
            }
            else
            {
                foreach (SlotHandler slot in this.slots)
                {
                    if (!slot.completed)
                        return;
                    else
                    {
                        this.boardOutline.SetActive(false);
                        this.boardCompleted.SetActive(true);
                    }
                }
            }
        }

        public void MoveToNextScene(string sceneName)
        {
            Utility.ChangeScene(sceneName);
        }

        private void onActivityComplete()
        {
            this.boardOutline.SetActive(false);
            this.boardCompleted.SetActive(true);
            if (SuccessAudioClip != null)
            {
                Debug.Log("Playing sound effect using audio manager");
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.PlaySoundEffect(SuccessAudioClip);
                    Utility.PlayOneShot(this.slotCompleteAudioClip);
                }
                else
                {
                    Utility.PlayOneShot(this.slotCompleteAudioClip);
                    Utility.PlayOneShot(this.SuccessAudioClip);
                }
            }

        }
    }
}