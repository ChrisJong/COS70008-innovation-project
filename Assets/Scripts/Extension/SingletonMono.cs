namespace Extension {

    using UnityEngine;

    public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T> {
        public static T instance { get; private set; }
        public virtual void Awake() {
            if (instance != null) {
                Debug.LogWarning(this.ToString() + "another instance is already on the scene, deleting this instance");
                GameObject go = this.gameObject;
                Destroy(this);
                Destroy(go);
                return;
            }
            instance = this as T;
        }
    }
}