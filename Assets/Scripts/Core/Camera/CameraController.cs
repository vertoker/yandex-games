using System;
using UnityEngine;

namespace Core.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private FollowMode posMode;
        [SerializeField] private FollowMode rotMode;
        [SerializeField] private CameraPreset preset;
        [Space]
        [SerializeField] private Transform target;
        private static CameraController _instance;
        
        private UnityEngine.Camera _cam;
        private Transform _self;
        
        public static Transform Target
        {
            get => _instance.target;
            set => _instance.target = value;
        }
        public static Transform Self
        {
            get => _instance._self;
            set => _instance._self = value;
        }
        public static UnityEngine.Camera SelfCamera
        {
            get => _instance._cam;
            set => _instance._cam = value;
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
            _cam = _self.GetComponent<UnityEngine.Camera>();
        }

        private void Update()
        {
            switch (posMode)
            {
                case FollowMode.Towards:
                    _self.position = Vector3.MoveTowards(_self.position, target.position, preset.pos.speed);
                    break;
                case FollowMode.Lerp:
                    _self.position = Vector3.Lerp(_self.position, target.position, preset.pos.speed);
                    break;
                case FollowMode.LerpUnclamped:
                    _self.position = Vector3.LerpUnclamped(_self.position, target.position, preset.pos.speed);
                    break;
            }
            
            switch (rotMode)
            {
                case FollowMode.Towards:
                    _self.rotation = Quaternion.RotateTowards(_self.rotation, target.rotation, preset.rot.speed);
                    break;
                case FollowMode.Lerp:
                    _self.rotation = Quaternion.Lerp(_self.rotation, target.rotation, preset.rot.speed);
                    break;
                case FollowMode.LerpUnclamped:
                    _self.rotation = Quaternion.LerpUnclamped(_self.rotation, target.rotation, preset.rot.speed);
                    break;
            }
        }
    }
    
    public enum FollowMode
    {
        None = 0,
        Towards = 1,
        Lerp = 2,
        LerpUnclamped = 3
    }
}
