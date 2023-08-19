using System;
using Core.Camera;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Drawer
{
    public class CameraInputManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private float farSensitivity;
        [SerializeField] private float closeSensitivity;
        [SerializeField] private CameraViewer camViewer;
        [SerializeField] private Transform camTarget;
        [SerializeField] private Scrollbar bar;
        
        private Vector2 _startPressPos;
        
        private void Awake()
        {
            CameraController.PosMode = FollowMode.Lerp;
        }

        private void Update()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
                bar.value = Mathf.Clamp(bar.value - scroll, 0, 1);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var offsetPress = GetLocalPress(Input.mousePosition);
            _startPressPos = offsetPress + (Vector2)camTarget.position;
        }
        public void OnDrag(PointerEventData eventData)
        {
            var offsetPress = GetLocalPress(Input.mousePosition);
            var currentPos = _startPressPos - offsetPress;
            var x = Mathf.Clamp(currentPos.x, -camViewer.CurrentSize.x, camViewer.CurrentSize.x);
            var y = Mathf.Clamp(currentPos.y, -camViewer.CurrentSize.y, camViewer.CurrentSize.y);
            var pos = new Vector3(x, y, 0);
            camTarget.position = pos;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            
        }

        private static Vector2 GetLocalPress(Vector2 pressPoint)
        {
            var size = CameraController.SelfCamera.orthographicSize;
            var aspect = CameraController.SelfCamera.aspect;
            var screen = new Vector2(Screen.width, Screen.height);
            
            var viewport = new Vector2(pressPoint.x / screen.x, pressPoint.y / screen.y);
            viewport = new Vector2(viewport.x * 2 - 1, viewport.y * 2 - 1);
            
            var offsetPos = viewport * new Vector2(size * aspect, size);
            return offsetPos;
        }
    }
}