namespace Manager
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using Extension;

    public class MatchManager : SingletonMono<MatchManager>
    {
        public int completedCount = 0;

        public List<SlotHandler> slots;

        public GameObject boardOutline;
        public GameObject boardCompleted;

        public Canvas mainCanvas;

        public void CheckMatches()
        {
            if(this.completedCount == this.slots.Count)
            {
                this.boardOutline.SetActive(false);
                this.boardCompleted.SetActive(true);
            } 
            else
            {
                foreach(SlotHandler slot in this.slots)
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

        public void BackToSelection()
        {
            GlobalManager.instance.ChangeScene("selection");
        }
    }
}