using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace Scripts.Game.UI
{
    public class InputReceiver : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerUpHandler, IPointerClickHandler
    {
        public static Action<PointerEventData> OnDownEvent;
        public static Action<PointerEventData> OnBeginDragEvent;
        public static Action<PointerEventData> OnDragEvent;
        public static Action<PointerEventData> OnUpEvent;
        public static Action<PointerEventData> OnClickEvent;

        public void OnPointerDown(PointerEventData eventData) => OnDownEvent?.Invoke(eventData);
        public void OnBeginDrag(PointerEventData eventData) => OnBeginDragEvent?.Invoke(eventData);
        public void OnDrag(PointerEventData eventData) => OnDragEvent?.Invoke(eventData);
        public void OnPointerUp(PointerEventData eventData) => OnUpEvent?.Invoke(eventData);
        public void OnPointerClick(PointerEventData eventData) => OnClickEvent?.Invoke(eventData);
    }
}