using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Scripts.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Game
{
    public class TriggerMover : MonoBehaviour
    {
        [SerializeField] private InputReceiver input;
        [SerializeField] private Camera cam;
        [SerializeField] LayerMask mask;
        [SerializeField] private List<Collider2D> items = new List<Collider2D>();
        private CircleCollider2D self;
        private Collider2D activeItem;

        private static UnityEvent<List<Collider2D>> touchesEvent = new UnityEvent<List<Collider2D>>();
        public static event UnityAction<List<Collider2D>> TouchesUpdate
        {
            add => touchesEvent.AddListener(value);
            remove => touchesEvent.RemoveListener(value);
        }

        private void OnEnable()
        {
            input.OnDownUpdate += Down;
            input.OnDragUpdate += Drag;
            input.OnUpUpdate += Up;
            ItemMover.ActiveItemUpdate += GetActiveItem;
        }
        private void OnDisable()
        {
            input.OnDownUpdate -= Down;
            input.OnDragUpdate -= Drag;
            input.OnUpUpdate -= Up;
            ItemMover.ActiveItemUpdate -= GetActiveItem;
        }
        private void Awake()
        {
            self = GetComponent<CircleCollider2D>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Item"))
            {
                items.Add(collision);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Item"))
            {
                items.Remove(collision);
            }
        }

        private void Down(PointerEventData data)
        {
            transform.position = cam.ScreenToWorldPoint(data.pointerCurrentRaycast.screenPosition);
            self.enabled = true;
        }
        private void GetActiveItem(Transform transform)
        {
            activeItem = transform.GetComponent<Collider2D>();
        }
        private void Drag(PointerEventData data)
        {
            transform.position = cam.ScreenToWorldPoint(data.pointerCurrentRaycast.screenPosition);
        }
        private void Up(PointerEventData data)
        {
            if (activeItem != null)
                items.Add(activeItem);
            touchesEvent.Invoke(items);
            self.enabled = false;
            activeItem = null;
            items.Clear();
        }
    }
}