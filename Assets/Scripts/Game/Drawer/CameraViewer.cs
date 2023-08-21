using System;
using Core.Camera;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Drawer
{
    public class CameraViewer : MonoBehaviour
    {
        [SerializeField] private Vector2 startOffset = new(0, 0.2f);
        [SerializeField] private float farDistanceStart = 3;
        [SerializeField] private float farDistanceDeltaByPixel = 0.04f;
        [SerializeField] private float closeDistance = 0.5f;
        [SerializeField] private Scrollbar bar;

        private float _farDistance;
        
        public Vector2 CurrentSize { get; private set; }

        private Transform _self;
        private Vector2 _size;
        private bool _isInit;

        public void Init(Vector2 size, int width, int height)
        {
            _size = size;
            _isInit = true;
            _farDistance = farDistanceStart + farDistanceDeltaByPixel 
                * Mathf.Max(width, height);
            ValueChanged(bar.value);
        }

        private void Awake()
        {
            _self = transform;
        }
        private void Start()
        {
            CameraController.Target = _self;
        }
        private void OnEnable()
        {
            bar.onValueChanged.AddListener(ValueChanged);
        }
        private void OnDisable()
        {
            bar.onValueChanged.RemoveListener(ValueChanged);
            _isInit = false;
        }

        private void ValueChanged(float value)
        {
            CurrentSize = _size * (1 - value);
            var position = _self.position;
            var x = Mathf.Clamp(position.x, -CurrentSize.x + startOffset.x, CurrentSize.x + startOffset.x);
            var y = Mathf.Clamp(position.y, -CurrentSize.y + startOffset.y, CurrentSize.y + startOffset.y);
            _self.position = new Vector3(x, y, 0);

            if (!_isInit) return;
            var lerp = closeDistance + (_farDistance - closeDistance) * value;
            CameraController.SelfCamera.orthographicSize = lerp;
        }
        
    }
}