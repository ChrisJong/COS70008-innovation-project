namespace Manager
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using Extension;
    using Utlis;

    public class MatchManager : SingletonMono<MatchManager>
    {
        public int completedCount = 0;

        public List<SlotHandler> slots;

        public GameObject boardOutline;
        public GameObject boardCompleted;

        public Canvas mainCanvas;

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
                AudioManager.instance.PlaySoundEffect(SuccessAudioClip);
            }

        }
    }
}