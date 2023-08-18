using System;
using Core.Camera;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Drawer
{
    public class CameraViewer : MonoBehaviour
    {
        [SerializeField] private float farDistance;
        [SerializeField] private float closeDistance;
        [SerializeField] private Scrollbar bar;
        
        public Vector2 CurrentSize { get; private set; }

        private Transform _self;
        private Vector2 _size;
        private bool _isInit;

        public void Init(Vector2 size)
        {
            _size = size;
            _isInit = true;
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
            var x = Mathf.Clamp(position.x, -CurrentSize.x, CurrentSize.x);
            var y = Mathf.Clamp(position.y, -CurrentSize.y, CurrentSize.y);
            _self.position = new Vector3(x, y, 0);

            if (!_isInit) return;
            var lerp = closeDistance + (farDistance - closeDistance) * value;
            CameraController.SelfCamera.orthographicSize = lerp;
        }
        
    }
}