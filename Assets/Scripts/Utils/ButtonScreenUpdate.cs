using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Utils
{
    public class ButtonScreenUpdate : MonoBehaviour
    {
        [SerializeField] private RectTransform info;
        [SerializeField] private RectTransform trash;

        private void OnEnable()
        {
            ScreenCaller.ScreenOrientationChanged += ScreenUpdate;
        }
        private void OnDisable()
        {
            ScreenCaller.ScreenOrientationChanged -= ScreenUpdate;
        }

        private void ScreenUpdate(bool vertical, float aspect)
        {
            if (vertical)
            {
                info.anchorMin = info.anchorMax = info.pivot = new Vector2(1f, 0.5f);
                trash.anchorMin = trash.anchorMax = trash.pivot = new Vector2(0f, 0.5f);
                info.anchoredPosition = trash.anchoredPosition = Vector2.zero;
            }
            else
            {
                info.anchorMin = info.anchorMax = info.pivot = new Vector2(0.5f, 1f);
                trash.anchorMin = trash.anchorMax = trash.pivot = new Vector2(0.5f, 0f);
                info.anchoredPosition = trash.anchoredPosition = Vector2.zero;
            }
        }
    }
}