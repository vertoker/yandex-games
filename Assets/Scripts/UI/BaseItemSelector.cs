using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI
{
    public class BaseItemSelector : MonoBehaviour
    {
        [SerializeField] private float distanceSelect = 150f;
        [SerializeField] private InputReceiver input;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Camera cam;
        private Image[] images, actives;
        private bool active = false;
        private Vector2 startScreenPos;
        private Vector2 spawnPos;

        private void OnEnable()
        {
            input.OnDownUpdate += Down;
            input.OnDragUpdate += Drag;
            input.OnUpUpdate += Up;
        }
        private void OnDisable()
        {
            input.OnDownUpdate -= Down;
            input.OnDragUpdate -= Drag;
            input.OnUpUpdate -= Up;
        }
        private void Start()
        {
            images = new Image[6];
            actives = new Image[4];
            images[0] = GetComponent<Image>();
            for (int i = 0; i < 5; i++)
                images[i + 1] = transform.GetChild(i).GetComponent<Image>();
            for (int i = 0; i < 4; i++)
                actives[i] = transform.GetChild(i + 5).GetComponent<Image>();
            Deactivate();
        }
        public void Down(PointerEventData data)
        {
            startScreenPos = ScreenToCanvasPoint(data.pointerCurrentRaycast.screenPosition);
        }
        public void Drag(PointerEventData data)
        {
            if (!active)
                return;

            Vector2 delta = ScreenToCanvasPoint(data.pointerCurrentRaycast.screenPosition) - startScreenPos;
            foreach (var active in actives)
                active.enabled = false;
            if (delta.magnitude < distanceSelect)
                return;
            if (delta.x > 0)
                if (delta.y > 0)
                    actives[0].enabled = true;
                else
                    actives[1].enabled = true;
            else
                if (delta.y < 0)
                    actives[2].enabled = true;
                else
                    actives[3].enabled = true;
        }
        public void Up(PointerEventData data)
        {
            if (actives[0].enabled)
                ItemSpawner.CreateItem("Вода", spawnPos);
            else if (actives[1].enabled)
                ItemSpawner.CreateItem("Земля", spawnPos);
            else if (actives[2].enabled)
                ItemSpawner.CreateItem("Огонь", spawnPos);
            else if (actives[3].enabled)
                ItemSpawner.CreateItem("Воздух", spawnPos);
            Deactivate();
        }

        public void Activate(Vector2 pressPosition)
        {
            GetComponent<RectTransform>().anchoredPosition = ScreenToCanvasPoint(pressPosition);
            spawnPos = cam.ScreenToWorldPoint(pressPosition);
            foreach (var image in images)
                image.enabled = true;
            foreach (var active in actives)
                active.enabled = false;
            active = true;
        }
        public void Deactivate()
        {
            foreach (var image in images)
                image.enabled = false;
            foreach (var active in actives)
                active.enabled = false;
            active = false;
        }

        private Vector2 ScreenToCanvasPoint(Vector2 screenPoint)
        {
            float xSize = 1080f * ((float)Screen.width / Screen.height);
            float x = screenPoint.x / Screen.width * xSize - xSize / 2f;
            float y = screenPoint.y / Screen.height * 1080f - 540f;
            return new Vector2(x, y);
        }
    }
}