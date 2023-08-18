using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Drawer
{
    public class ReCenterButton : MonoBehaviour
    {
        [SerializeField] private float timeScale = 0.8f;
        [SerializeField] private float farDistance;
        [SerializeField] private float closeDistance;
        [SerializeField] private Scrollbar bar;
        [SerializeField] private RectTransform imagePreset;
        private RectTransform[] _icons;
        private Coroutine _update;

        private void Awake()
        {
            var parent = transform;
            _icons = new RectTransform[9];

            for (var i = 0; i < 9; i++)
            {
                _icons[i] = Instantiate(imagePreset, parent);
            }
        }

        private void OnEnable()
        {
            bar.onValueChanged.AddListener(ValueChanged);
            ValueChanged(bar.value);
        }
        private void OnDisable()
        {
            bar.onValueChanged.RemoveListener(ValueChanged);
        }

        private void ValueChanged(float value)
        {
            var lerp = farDistance + (closeDistance - farDistance) * value;

            _icons[0].anchoredPosition = new Vector2(-lerp, lerp);
            _icons[1].anchoredPosition = new Vector2(0, lerp);
            _icons[2].anchoredPosition = new Vector2(lerp, lerp);
            _icons[3].anchoredPosition = new Vector2(-lerp, 0);
            _icons[4].anchoredPosition = new Vector2(0, 0);
            _icons[5].anchoredPosition = new Vector2(lerp, 0);
            _icons[6].anchoredPosition = new Vector2(-lerp, -lerp);
            _icons[7].anchoredPosition = new Vector2(0, -lerp);
            _icons[8].anchoredPosition = new Vector2(lerp, -lerp);
        }

        private void CancelAnim()
        {
            if (_update != null)
                StopCoroutine(_update);
            bar.interactable = true;
        }

        public void ToggleScrollAnim()
        {
            CancelAnim();
            bar.interactable = false;
            _update = StartCoroutine(ScrollAnim(bar.value, bar.value > 0.5f ? 0 : 1));
        }

        private IEnumerator ScrollAnim(float start, float end)
        {
            var timeToMove = Mathf.Abs(end - start);
            for (var i = 0f; i <= 1; i += Time.deltaTime / timeToMove / timeScale)
            {
                bar.value = start + (end - start) * i;
                yield return null;
            }

            bar.value = end;
            CancelAnim();
        }
    }
}