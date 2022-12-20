using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using Utility;
using Vector3 = System.Numerics.Vector3;

namespace Game
{
    public class FuelSlider : MonoBehaviour
    {
        [SerializeField] private Image slider;
        [SerializeField] private Image background;
        [SerializeField] private Camera cam;
        [SerializeField] private Transform target;
        [SerializeField] private Vector2 offset = new Vector2(100f, -100f);
        
        private RectTransform tr;
        private Coroutine updater;
        private Vector2 screen;
        private Vector2 canvas;

        private void Awake()
        {
            tr = GetComponent<RectTransform>();
            background.enabled = false;
            slider.enabled = false;
        }
        
        private void OnEnable()
        {
            GameCycle.StartCycleEvent += StartUpdater;
            GameCycle.EndCycleEvent += StopUpdater;
            GameCycle.UpdateFuelEvent += UpdateSlider;
            ScreenCaller.ScreenOrientationChanged += ScreenChange;
        }

        private void OnDisable()
        {
            GameCycle.StartCycleEvent -= StartUpdater;
            GameCycle.EndCycleEvent -= StopUpdater;
            GameCycle.UpdateFuelEvent -= UpdateSlider;
        }

        private void UpdateSlider(float value)
        {
            slider.fillAmount = value;
        }
        private void StartUpdater()
        {
            background.enabled = true;
            slider.enabled = true;
            updater = StartCoroutine(UpdaterPosition());
        }
        private void StopUpdater()
        {
            if (updater != null)
                StopCoroutine(updater);
            background.enabled = false;
            slider.enabled = false;
        }
        private void ScreenChange(bool isVertical, float aspect)
        {
            if (isVertical)
            {
                screen = new Vector2(Screen.height, Screen.width);
                canvas = new Vector2(1080f, 1080f * aspect);
            }
            else
            {
                screen = new Vector2(Screen.width, Screen.height);
                canvas = new Vector2(1080f * aspect, 1080f);
            }
        }
        
        private IEnumerator UpdaterPosition()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                Vector2 pos = cam.WorldToScreenPoint(target.position);
                tr.anchoredPosition = pos * canvas / screen + offset;
            }
        }
    }
}
