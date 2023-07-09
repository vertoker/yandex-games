using Camera.Watchers;
using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private FollowMode posMode;
        [SerializeField] private FollowMode rotMode;
        [SerializeField] private CameraPreset preset;
        
        private WatcherBase _watcher;
        private static CameraController _instance;
        private Transform _self;
        
        public static void Switch(WatcherBase watcherNext)
        {
            if (_instance._watcher != null)
                _instance._watcher.EndSwitch();
            
            if (watcherNext != null)
                watcherNext.BeginSwitch();
            
            _instance._watcher = watcherNext;
        }
        
        public static FollowMode PosMode
        {
            get => _instance.posMode;
            set => _instance.posMode = value;
        }
        public static FollowMode RotMode
        {
            get => _instance.rotMode;
            set => _instance.rotMode = value;
        }
        
        public static VectorFollow PosPreset
        {
            get => _instance.preset.pos;
            set => _instance.preset.pos = value;
        }
        public static VectorFollow RotPreset
        {
            get => _instance.preset.rot;
            set => _instance.preset.rot = value;
        }

        private void Awake()
        {
            if (_instance != null)
                return;
            _instance = this;
            
            _self = transform;
        }

        private void FixedUpdate()
        {
            if (_watcher == null)
                return;
            
            switch (posMode)
            {
                case FollowMode.Instant:
                    _self.position = _watcher.Target.position;
                    break;
                case FollowMode.Towards:
                    _self.position = Vector3.MoveTowards(_self.position, _watcher.Target.position, preset.pos.speed);
                    break;
                case FollowMode.Lerp:
                    _self.position = Vector3.Lerp(_self.position, _watcher.Target.position, preset.pos.speed);
                    break;
                case FollowMode.LerpUnclamped:
                    _self.position = Vector3.LerpUnclamped(_self.position, _watcher.Target.position, preset.pos.speed);
                    break;
            }
            
            switch (rotMode)
            {
                case FollowMode.Instant:
                    _self.rotation = _watcher.Target.rotation;
                    break;
                case FollowMode.Towards:
                    _self.rotation = Quaternion.RotateTowards(_self.rotation, _watcher.Target.rotation, preset.rot.speed);
                    break;
                case FollowMode.Lerp:
                    _self.rotation = Quaternion.Lerp(_self.rotation, _watcher.Target.rotation, preset.rot.speed);
                    break;
                case FollowMode.LerpUnclamped:
                    _self.rotation = Quaternion.LerpUnclamped(_self.rotation, _watcher.Target.rotation, preset.rot.speed);
                    break;
            }
        }
    }
    
    public enum FollowMode
    {
        None,
        Instant,
        Towards,
        Lerp,
        LerpUnclamped
    }
}
