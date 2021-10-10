namespace Manager
{

    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Extension;

    public class DraggingManager : SingletonMono<DraggingManager>
    {
        public List<SlotHandler> slots;

        public List<SlotToken> tokens;

        public bool completed = false;

        public AudioClip SuccessAudioClip;

        public void CheckMatches()
        {
            if (this.slots.Count >= 0)
            {

                foreach (SlotHandler slot in this.slots)
                {
                    if (!slot.completed)
                        return;
                }

                onActivityComplete();
            }
            else
            {
                Debug.LogWarning("No Slots Found!");
            }
        }

        public void BackToSelection()
        {
            if (GlobalManager.instance != null)
                GlobalManager.instance.ChangeScene("selection");
        }

        private void ToggleDraggable(bool canDrag)
        {
            if (this.tokens.Count >= 0)
            {
                foreach (SlotToken token in this.tokens)
                    token.draggable = canDrag;
            }
        }

        public void MoveToNextScene(string sceneName)
        {
            GlobalManager.instance.ChangeScene(sceneName);
        }

        private void onActivityComplete()
        {
            Debug.Log("Completed");
            this.completed = true;
            ToggleDraggable(false);
            if (SuccessAudioClip != null)
            {
                Debug.Log("Playing sound effect using audio manager");
                AudioManager.Instance.PlaySoundEffect(SuccessAudioClip);
            }

        }
    }
}