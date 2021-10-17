namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Utils;

    public class MusiceliManager : MonoBehaviour
    {
        public bool keyPressed = false;

        public GameObject boardOutline;
        public GameObject boardCompleted;

        public Canvas mainCanvas;

        public void CheckMatches()
        {
            if (keyPressed)
            {
                this.boardOutline.SetActive(false);
                this.boardCompleted.SetActive(true);
            }
        }

        public void BackToSelection()
        {
            Utility.ChangeScene("selection");
        }

        public void KeyPress()
        {
            this.keyPressed = !this.keyPressed;
            CheckMatches();
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
    }
}