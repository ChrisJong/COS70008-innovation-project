namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using Extension;
    using Utlis;

    public class PuzzleManager : SingletonMono<PuzzleManager>
    {
        public bool completed = false;

        public Image completedPanel;

        public List<SlotHandler> slots;

        public Sprite completedImage;

        public AudioClip successAduioClip;

        public void Start()
        {
            if (this.completedPanel != null && this.completedImage != null)
                this.completedPanel.sprite = this.completedImage;
        }

        public void CheckMatches()
        {
            if(this.slots.Count >= 0)
            {
                foreach(SlotHandler slot in this.slots)
                {
                    if (!slot.completed)
                        return;
                }

                onActivityComplete();
            }
        }

        public void LoadScene(string sceneName)
        {
            Utility.ChangeScene(sceneName);
        }

        private void onActivityComplete()
        {
            Debug.Log("Completed");
            this.completed = true;
            if (this.successAduioClip != null)
            {
                Debug.Log("Playing sound effect using audio manager");
                if(AudioManager.instance != null)
                    AudioManager.instance.PlaySoundEffect(this.successAduioClip);
            }

        }
    }
}