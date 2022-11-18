using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Scripts.Items;
using UnityEngine.Events;
using UnityEngine.Experimental.AI;
using Scripts.Game;
using System.Linq;

namespace Scripts.UI
{
    public class ItemMover : MonoBehaviour
    {
        [SerializeField] private BaseItemSelector itemSelector;
        [SerializeField] private InputReceiver input;
        [SerializeField] private ItemSpawner spawner;
        [SerializeField] LayerMask mask;
        [SerializeField] Camera cam;

        [Space]
        [SerializeField] private float timeWaitDublicate = 0.5f;
        [SerializeField] private float timeWaitSelector = 1f;
        [SerializeField] private float distanceTriggerDrag = 0.3f;
        [SerializeField] private float radiusOffset = 0.2f;
        [SerializeField] private float lerpTimePos = 0.2f;
        [SerializeField] private float lerpTimeRot = 0.2f;
        [SerializeField] private float rotActiveTrigger = 0.1f;
        [SerializeField] private Vector2 offsetItemText = new Vector2(0f, -0.7f);
        [SerializeField] private Vector2 offsetShadowOn = new Vector2(0.1f, -0.1f);
        [SerializeField] private Vector2 offsetShadowOff = new Vector2(0.2f, -0.2f);

        private Transform activeItem;
        private Vector3 startPosition;
        private Vector3 lastPosition;
        private Coroutine dragUpdate;
        private Coroutine selectorTrigger;
        private Coroutine createDublicate;
        private bool isPressed = false;
        private bool canDublicate = true;

        private static UnityEvent<Transform> activeItemEvent = new UnityEvent<Transform>();
        public static event UnityAction<Transform> ActiveItemUpdate
        {
            add => activeItemEvent.AddListener(value);
            remove => activeItemEvent.RemoveListener(value);
        }

        private void OnEnable()
        {
            input.OnDownUpdate += OnDown;
            input.OnBeginDragUpdate += OnBeginDrag;
            input.OnDragUpdate += OnDrag;
            input.OnUpUpdate += OnUp;
            TriggerMover.TouchesUpdate += TriggerOnUp;

        }
        private void OnDisable()
        {
            input.OnDownUpdate -= OnDown;
            input.OnBeginDragUpdate -= OnBeginDrag;
            input.OnDragUpdate -= OnDrag;
            input.OnUpUpdate -= OnUp;
            TriggerMover.TouchesUpdate -= TriggerOnUp;
        }

        private void OnDown(PointerEventData data)
        {
            activeItem = null;
            isPressed = true;
            pressPosition = data.pointerCurrentRaycast.screenPosition;
            if (selectorTrigger != null)
                StopCoroutine(selectorTrigger);
            var collider = Physics2D.Raycast(cam.ScreenToWorldPoint(data.pointerCurrentRaycast.screenPosition), Vector2.zero, 0.1f, mask).collider;
            if (collider != null)
            {
                activeItem = collider.gameObject.transform;
                activeItemEvent.Invoke(activeItem);
                spawner.DisableItem(activeItem.gameObject);
                lastPosition = startPosition = activeItem.position;
                dragUpdate = StartCoroutine(DragUpdate());
            }
            else
            {
                selectorTrigger = StartCoroutine(SelectorTrigger(data.pointerCurrentRaycast.screenPosition));
            }
        }
        private IEnumerator DragUpdate()
        {
            while (true)
            {
                yield return null;
                OnDrag();
            }
        }
        private IEnumerator SelectorTrigger(Vector2 pressPosition)
        {
            yield return new WaitForSeconds(timeWaitSelector);
            if ((cam.ScreenToWorldPoint(pressPosition) - startPosition).magnitude > distanceTriggerDrag && isPressed)
                itemSelector.Activate(pressPosition);
        }

        private Vector2 pressPosition;
        private void OnBeginDrag(PointerEventData data)
        {
            pressPosition = data.pointerCurrentRaycast.screenPosition;
        }
        private void OnDrag(PointerEventData data)
        {
            pressPosition = data.pointerCurrentRaycast.screenPosition;
        }

        private void OnDrag()
        {
            if (activeItem == null)
                return;

            var direction = activeItem.position - lastPosition;
            float angle = Mathf.Atan2(direction.y, direction.x);
            lastPosition = activeItem.position;
            GetCoordPhysicOffset(angle, radiusOffset, out Vector2 offsetPosition, out Quaternion rotation);
            var pos = Vector2.LerpUnclamped(activeItem.position, (Vector2)cam.ScreenToWorldPoint(pressPosition) + offsetPosition, lerpTimePos);
            var rot = Quaternion.LerpUnclamped(activeItem.rotation, rotation, lerpTimeRot);
            activeItem.SetPositionAndRotation(pos, direction.magnitude > rotActiveTrigger ? rot : activeItem.rotation);
            activeItem.GetChild(0).rotation = Quaternion.identity;
            activeItem.GetChild(0).position = pos + offsetItemText;
            activeItem.GetChild(1).position = pos + offsetShadowOn;
        }
        private void OnUp(PointerEventData data)
        {
            isPressed = false;
            if (activeItem == null)
                return;

            StopCoroutine(dragUpdate);
            if (selectorTrigger != null)
                StopCoroutine(selectorTrigger);

            if ((lastPosition - startPosition).magnitude < distanceTriggerDrag && canDublicate)
            {
                canDublicate = false;
                ItemSpawner.CreateItem(activeItem.name, lastPosition);
                if (createDublicate != null)
                    StopCoroutine(createDublicate);
                createDublicate = StartCoroutine(TriggerCreateDublicateUp());
            }

            activeItem.GetChild(1).position = activeItem.position + (Vector3)offsetShadowOff;
        }
        private IEnumerator TriggerCreateDublicateUp()
        {
            yield return new WaitForSeconds(timeWaitDublicate);
            canDublicate = true;
        }
        private void TriggerOnUp(List<Collider2D> list)
        {
            if (activeItem == null)
                return;
            spawner.EnableItem(activeItem.gameObject);

            if (!canDublicate)
                return;

            //Debug.Log(list.Count);
            List<ContactData> cd = new List<ContactData>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CompareTag("Item"))
                {
                    cd.Add(new ContactData()
                    {
                        distance = Vector2.Distance(list[i].gameObject.transform.position, transform.position),
                        tr = list[i].transform
                    });
                }
            }
            cd.OrderBy(p => p.distance);

            if (cd.Count >= 3)
            {
                var pos = (cd[0].tr.position + cd[1].tr.position + cd[2].tr.position) / 3f;
                ItemSpawner.ExecuteRecipe(cd[0].tr.name, cd[1].tr.name, cd[2].tr.name, pos, cd[0].tr.gameObject, cd[1].tr.gameObject, cd[2].tr.gameObject);
            }
            else if (cd.Count == 2)
            {
                var pos = (cd[0].tr.position + cd[1].tr.position) / 2f;
                ItemSpawner.ExecuteRecipe(cd[0].tr.name, cd[1].tr.name, string.Empty, pos, cd[0].tr.gameObject, cd[1].tr.gameObject, null);
            }
        }
        struct ContactData
        {
            public float distance;
            public Transform tr;
        }

        private static void GetCoordPhysicOffset(float angle, float radius, out Vector2 offsetPosition, out Quaternion rotation)
        {
            offsetPosition = new Vector2(Mathf.Cos(angle - 90f), Mathf.Sin(angle - 90f)) * radius;
            rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90f);
        }
    }
}