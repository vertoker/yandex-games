using System;
using UnityEngine;

namespace Core.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private FollowMode posMode;
        [SerializeField] private FollowMode rotMode;
        [SerializeField] private CameraPreset preset;
        [Space]
        [SerializeField] private Transform target;
        private static CameraController _instance;
        private Transform _self;
        
        public static Transform Target
        {
            get => _instance.target;
            set => _instance.target = value;
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
            get => _instance.preset.Pos;
            set => _instance.preset.Pos = value;
        }
        public static VectorFollow RotPreset
        {
            get => _instance.preset.Rot;
            set => _instance.preset.Rot = value;
        }

        private void Awake()
        {
            _self = transform;
            _instance = this;
        }

        private void Update()
        {
            switch (posMode)
            {
                case FollowMode.None:
                    break;
                case FollowMode.MoveTowards:
                    break;
                case FollowMode.Lerp:
                    break;
                case FollowMode.LerpUnclamped:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(posMode), posMode, null);
            }
            
            switch (rotMode)
            {
                case FollowMode.None:
                    break;
                case FollowMode.MoveTowards:
                    break;
                case FollowMode.Lerp:
                    break;
                case FollowMode.LerpUnclamped:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rotMode), rotMode, null);
            }
        }
    }
    
    public enum FollowMode
    {
        None = 0,
        MoveTowards = 1,
        Lerp = 2,
        LerpUnclamped = 3
    }
}
