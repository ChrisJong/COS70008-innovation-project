namespace Manager
{

    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Extension;
    using Utils;

    public class DraggingManager : SingletonMono<DraggingManager>
    {
        [Header("Main Components")]
        public List<SlotHandler> slots;

        public List<SlotToken> tokens;

        public bool completed = false;

        [Space(10), Header("Audio")]
        public AudioClip SuccessAudioClip;

        [Space(10), Header("Particles")]
        public ParticleSystem successParticleSystem;

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
            Debug.Log("SelectionManager.instance: " + SelectionManager.instance);
            Debug.Log(SelectionManager.instance.NameWritingQueue.ToArray().ToString());
            if (SelectionManager.instance != null)
            {
                Debug.Log(SelectionManager.instance.NameWritingQueue.ToArray().ToString());
                if (SelectionManager.instance.NameWritingQueue.Count > 0)
                {
                    sceneName = SelectionManager.instance.NameWritingQueue.Dequeue();
                    Debug.Log("Next scene name came from Queue: " + sceneName);
                }
            }
            Utility.ChangeScene(sceneName);
        }

        private void onActivityComplete()
        {
            Debug.Log("Completed");
            this.completed = true;
            ToggleDraggable(false);
            if (SuccessAudioClip != null)
            {
                Debug.Log("Playing sound effect using audio manager");
                if (AudioManager.instance != null)
                    AudioManager.instance.PlaySoundEffect(SuccessAudioClip);
                else
                    Utility.PlayOneShot(this.SuccessAudioClip, 0.5f);
            }
            if (successParticleSystem != null)
            {
                Debug.Log("Playing Particle System");
                successParticleSystem.Play();
            }

        }
    }
}