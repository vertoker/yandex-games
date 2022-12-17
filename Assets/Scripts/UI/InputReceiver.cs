using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.UI
{
    public class InputReceiver : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerUpHandler, IPointerClickHandler
    {
        private UnityEvent<PointerEventData> onDownEvent = new UnityEvent<PointerEventData>();
        private UnityEvent<PointerEventData> onBeginDragEvent = new UnityEvent<PointerEventData>();
        private UnityEvent<PointerEventData> onDragEvent = new UnityEvent<PointerEventData>();
        private UnityEvent<PointerEventData> onUpEvent = new UnityEvent<PointerEventData>();
        private UnityEvent<PointerEventData> onClickEvent = new UnityEvent<PointerEventData>();

        public event UnityAction<PointerEventData> OnDownUpdate
        {
            add => onDownEvent.AddListener(value);
            remove => onDownEvent.RemoveListener(value);
        }
        public event UnityAction<PointerEventData> OnBeginDragUpdate
        {
            add => onBeginDragEvent.AddListener(value);
            remove => onBeginDragEvent.RemoveListener(value);
        }
        public event UnityAction<PointerEventData> OnDragUpdate
        {
            add => onDragEvent.AddListener(value);
            remove => onDragEvent.RemoveListener(value);
        }
        public event UnityAction<PointerEventData> OnUpUpdate
        {
            add => onUpEvent.AddListener(value);
            remove => onUpEvent.RemoveListener(value);
        }
        public event UnityAction<PointerEventData> OnClickUpdate
        {
            add => onClickEvent.AddListener(value);
            remove => onClickEvent.RemoveListener(value);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onDownEvent.Invoke(eventData);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDragEvent.Invoke(eventData);
        }
        public void OnDrag(PointerEventData eventData)
        {
            onDragEvent.Invoke(eventData);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            onUpEvent.Invoke(eventData);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            onClickEvent.Invoke(eventData);
        }
    }
}