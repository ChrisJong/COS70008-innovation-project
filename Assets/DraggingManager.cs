namespace Manager {

    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Extension;

    public class DraggingManager : SingletonMono<DraggingManager>
    {
        public List<SlotHandler> slots;

        public List<SlotToken> tokens;

        public bool completed = false;

        public void CheckMatches()
        {
            if (this.slots.Count >= 0) {

                foreach (SlotHandler slot in this.slots)
                {
                    if (!slot.completed)
                        return;
                }

                Debug.Log("Completed");
                this.completed = true;
                ToggleDraggable(false);
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
    }
}