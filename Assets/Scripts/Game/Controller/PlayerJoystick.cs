using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Scripts.Game.UI;
using Scripts.Tools;

namespace Scripts.Game.Controller
{
    public class PlayerJoystick : MonoBehaviour
    {
        [SerializeField] private float joystickBorder = 800f;
        [SerializeField] private PersonController controller;
        [SerializeField] private Transform cam;

        private bool vertical;
        private Vector2 startScreenPos;
        private Vector2 currentDirection;
        private Coroutine updater;

        private void OnEnable()
        {
            InputReceiver.OnDownEvent += Down;
            InputReceiver.OnBeginDragEvent += BeginDrag;
            InputReceiver.OnDragEvent += Drag;
            InputReceiver.OnUpEvent += Up;
            ScreenCaller.ScreenOrientationChanged += ScreenUpdate;
        }
        private void OnDisable()
        {
            InputReceiver.OnDownEvent -= Down;
            InputReceiver.OnBeginDragEvent -= BeginDrag;
            InputReceiver.OnDragEvent -= Drag;
            InputReceiver.OnUpEvent -= Up;
            ScreenCaller.ScreenOrientationChanged -= ScreenUpdate;

            if (updater != null)
            {
                StopCoroutine(updater);
                updater = null;
            }
        }

        private void Down(PointerEventData data)
        {
            startScreenPos = ScreenToCanvasPoint(data.pointerCurrentRaycast.screenPosition);
        }
        private void BeginDrag(PointerEventData data)
        {
            if (updater == null)
                updater = StartCoroutine(UpdateJoystick());
        }
        private void Drag(PointerEventData data)
        {
            var screenPos = ScreenToCanvasPoint(data.pointerCurrentRaycast.screenPosition);
            var deltaX = Mathf.Clamp((screenPos.x - startScreenPos.x) / joystickBorder, -1f, 1f);
            var deltaY = Mathf.Clamp((screenPos.y - startScreenPos.y) / joystickBorder, -1f, 1f);
            currentDirection = new Vector2(deltaX, deltaY);
        }
        private void Up(PointerEventData data)
        {
            if (updater != null)
            {
                StopCoroutine(updater);
                updater = null;
            }
            currentDirection.Normalize();
            controller.Move(Math.RotateVector(currentDirection / 100f, -cam.eulerAngles.y));
            startScreenPos = currentDirection = Vector2.zero;
        }
        private IEnumerator UpdateJoystick()
        {
            while (true)
            {
                yield return null;
                controller.Move(Math.RotateVector(currentDirection,- cam.eulerAngles.y));
            }
        }

        private void ScreenUpdate(bool vertical, float aspect)
        {
            this.vertical = vertical;
        }
        private Vector2 ScreenToCanvasPoint(Vector2 screenPoint)
        {
            float maxSize, x, y;
            if (vertical)
            {
                maxSize = 1080f * ((float)Screen.height / Screen.width);
                y = screenPoint.y / Screen.height * maxSize - maxSize / 2f;
                x = screenPoint.x / Screen.width * 1080f - 540f;
                return new Vector2(x, y);
            }
            maxSize = 1080f * ((float)Screen.width / Screen.height);
            x = screenPoint.x / Screen.width * maxSize - maxSize / 2f;
            y = screenPoint.y / Screen.height * 1080f - 540f;
            return new Vector2(x, y);
        }
    }
}