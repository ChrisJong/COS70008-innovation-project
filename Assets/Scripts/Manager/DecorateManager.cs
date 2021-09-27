namespace Manager {

    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Extension;

    public class DecorateManager : SingletonMono<DecorateManager> {

        [SerializeField] private DrawManager _drawManager;
        [SerializeField] private Decorate _decorate;

        public override void Init() {
            throw new System.NotImplementedException();
        }

        public void Check(List<DrawPoints> drawCollection)
        {
            foreach(DrawPoints drawpoint in drawCollection)
            {
                this._decorate.Check(drawpoint);
            }

            if (this._decorate.finished)
            {
                this._drawManager.ClearLines();
            }
        }
    }
}