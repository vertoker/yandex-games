using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace Scripts.UI
{
    public class InputReceiver : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerUpHandler, IPointerClickHandler
    {
        public static Action<PointerEventData> onDownEvent;
        public static Action<PointerEventData> onBeginDragEvent;
        public static Action<PointerEventData> onDragEvent;
        public static Action<PointerEventData> onUpEvent;
        public static Action<PointerEventData> onClickEvent;

        public void OnPointerDown(PointerEventData eventData) => onDownEvent.Invoke(eventData);
        public void OnBeginDrag(PointerEventData eventData) => onBeginDragEvent.Invoke(eventData);
        public void OnDrag(PointerEventData eventData) => onDragEvent.Invoke(eventData);
        public void OnPointerUp(PointerEventData eventData) => onUpEvent.Invoke(eventData);
        public void OnPointerClick(PointerEventData eventData) => onClickEvent.Invoke(eventData);
    }
}