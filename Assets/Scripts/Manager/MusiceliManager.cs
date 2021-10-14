    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

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


        public void KeyPress()
        {
            this.keyPressed = !this.keyPressed;
            CheckMatches();
        }
    }