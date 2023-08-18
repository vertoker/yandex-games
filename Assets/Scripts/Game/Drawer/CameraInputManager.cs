using System;
using Core.Camera;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Drawer
{
    public class CameraInputManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private CameraViewer camViewer;
        [SerializeField] private Transform camTarget;
        private Vector3 _startOffsetPos;
        private Vector3 _startPos;

        private void Awake()
        {
            CameraController.PosMode = FollowMode.Towards;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            CameraController.PosMode = FollowMode.None;
            _startPos = CameraController.SelfCamera.ScreenToWorldPoint(Input.mousePosition);
            _startOffsetPos = camTarget.position;
        }
        public void OnDrag(PointerEventData eventData)
        {
            var worldPress = CameraController.SelfCamera.WorldToScreenPoint(Input.mousePosition);
            var currentPos = worldPress - _startPos;
            var currentOffsetPos = _startOffsetPos - currentPos;
            var x = Mathf.Clamp(currentOffsetPos.x, -camViewer.CurrentSize.x, camViewer.CurrentSize.x);
            var y = Mathf.Clamp(currentOffsetPos.y, -camViewer.CurrentSize.y, camViewer.CurrentSize.y);
            var pos = new Vector3(x, y, 0);
            CameraController.Self.position = pos;
            camTarget.position = pos;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            CameraController.PosMode = FollowMode.Towards;
        }
    }
}