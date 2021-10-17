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

        public Image matchImage;

        [Space(10), Header("Editable")]
        public List<SlotHandler> slots;

        public Sprite outlineSprite;
        public Sprite completeSprite;

        [Space(10), Header("Audio")]
        public AudioClip slotCompleteAudioClip;
        public AudioClip SuccessAudioClip;

        [Space(10), Header("Particles")]
        public ParticleSystem successParticleSystem;

        public void Start()
        {
            this.matchImage.sprite = this.outlineSprite;
        }

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
                        onActivityComplete();
                    }
                }
            }
        }

        public void MoveToNextScene(string sceneName)
        {
            Debug.Log("SelectionManager.instance: " + SelectionManager.instance);
            Debug.Log(SelectionManager.instance.SceneQueue.ToArray().ToString());
            if (SelectionManager.instance != null)
            {
                Debug.Log(SelectionManager.instance.SceneQueue.ToArray().ToString());
                if (SelectionManager.instance.SceneQueue.Count > 0)
                {
                    sceneName = SelectionManager.instance.SceneQueue.Dequeue();
                    Debug.Log("Next scene name came from Queue: " + sceneName);
                }
            }
            Utility.ChangeScene(sceneName);
        }

        private void onActivityComplete()
        {
            this.matchImage.sprite = this.completeSprite;
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
                    Utility.PlayOneShot(this.SuccessAudioClip, 0.5f);
                }
            }
            if (successParticleSystem != null)
            {
                Debug.Log("Playing Particle System");
                successParticleSystem.Play();
            }

        }
    }
}