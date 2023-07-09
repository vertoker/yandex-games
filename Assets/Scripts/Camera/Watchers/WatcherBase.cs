using UnityEngine;

namespace Camera.Watchers
{
    public abstract class WatcherBase : MonoBehaviour
    {
        public Transform Target { get; private set; }

        private void Awake()
        {
            Target = transform;
        }

        public abstract void BeginSwitch();
        public abstract void UpdateWatch();
        public abstract void EndSwitch();
    }
}
