using System;
using UnityEngine.UI;
using UnityEngine;
using Utility;

namespace UI
{
    public class ScreenFitter : MonoBehaviour
    {
        private RectTransform self;

        private void Awake()
        {
            self = GetComponent<RectTransform>();
        }
        private void OnEnable()
        {
            ScreenCaller.ScreenOrientationChanged += ScreenUpdate;
        }
        private void OnDisable()
        {
            ScreenCaller.ScreenOrientationChanged -= ScreenUpdate;
        }
        private void ScreenUpdate(bool isVertical, float aspect)
        {
            self.sizeDelta = new Vector2(1080f * aspect, 1080f);
        }
    }
}
