using UnityEngine;

namespace Core.Camera.Watchers
{
    public abstract class WatcherBase : MonoBehaviour
    {
        public Transform Target => transform;

        public abstract void BeginSwitch();
        public abstract void UpdateWatch();
        public abstract void EndSwitch();
    }
}
