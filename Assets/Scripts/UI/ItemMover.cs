using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Scripts.Items;
using UnityEngine.UI;
using UnityEditor.PackageManager;
using static UnityEditor.PlayerSettings;

namespace Scripts.UI
{
    public class ItemMover : MonoBehaviour
    {
        [SerializeField] private InputReceiver input;
        [SerializeField] private ItemSpawner spawner;
        [SerializeField] LayerMask mask;
        [SerializeField] Camera cam;

        [Space]
        [SerializeField] private float distanceTriggerDrag = 0.3f;
        [SerializeField] private float radiusOffset = 0.2f;
        [SerializeField] private float lerpTimePos = 0.2f;
        [SerializeField] private float lerpTimeRot = 0.2f;
        [SerializeField] private float rotActiveTrigger = 0.1f;
        [SerializeField] private Vector2 offsetItemText = new Vector2(0f, -0.7f);

        private Transform activeItem;
        private Vector3 startPosition;
        private Vector3 lastPosition;
        private Coroutine dragUpdate;


        private void OnEnable()
        {
            input.OnDownUpdate += OnDown;
            //input.OnDragUpdate += OnDrag;
            input.OnUpUpdate += OnUp;
            //input.OnClickUpdate += OnClick;
        }
        private void OnDisable()
        {
            input.OnDownUpdate -= OnDown;
            //input.OnDragUpdate -= OnDrag;
            input.OnUpUpdate -= OnUp;
            //input.OnClickUpdate -= OnClick;
        }

        private void OnDown(PointerEventData data)
        { 
            var collider = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0.1f, mask).collider;
            if (collider != null)
            {
                activeItem = collider.gameObject.transform;
                lastPosition = startPosition = activeItem.position;
                spawner.DisableItem(collider.gameObject);
                dragUpdate = StartCoroutine(DragUpdate());
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
        private void OnDrag()
        {
            if (activeItem == null)
                return;

            var direction = activeItem.position - lastPosition;
            float angle = Mathf.Atan2(direction.y, direction.x);
            //Debug.Log(direction.magnitude);
            lastPosition = activeItem.position;
            GetCoordPhysicOffset(angle, radiusOffset, out Vector2 offsetPosition, out Quaternion rotation);
            var pos = Vector2.LerpUnclamped(activeItem.position, (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) + offsetPosition, lerpTimePos);
            var rot = Quaternion.LerpUnclamped(activeItem.rotation, rotation, lerpTimeRot);
            activeItem.SetPositionAndRotation(pos, direction.magnitude > rotActiveTrigger ? rot : activeItem.rotation);
            activeItem.GetChild(0).rotation = Quaternion.identity;
            activeItem.GetChild(0).position = pos + offsetItemText;
        }
        private void OnUp(PointerEventData data)
        {
            if (activeItem == null)
                return;

            StopCoroutine(dragUpdate);
            //activeItem.SetPositionAndRotation(cam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);

            if ((lastPosition - startPosition).magnitude < distanceTriggerDrag)
                ItemSpawner.CreateItem(activeItem.name, lastPosition);
            else
                activeItem.GetComponent<Item>().Activate();

            spawner.EnableItem(activeItem.gameObject);
            activeItem = null;
        }

        private static void GetCoordPhysicOffset(float angle, float radius, out Vector2 offsetPosition, out Quaternion rotation)
        {
            offsetPosition = new Vector2(Mathf.Cos(angle - 90f), Mathf.Sin(angle - 90f)) * radius;
            rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90f);
        }
    }
}