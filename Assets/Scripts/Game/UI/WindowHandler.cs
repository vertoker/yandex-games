using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Tools;

namespace Scripts.Game.UI
{
    public class WindowHandler : MonoBehaviour
    {
        private static WindowHandler self;
        private RectTransform windowContainer;
        private AnimatorUI an;

        private void Awake()
        {
            self = this;
            windowContainer = GetComponent<RectTransform>();
            an = GetComponent<AnimatorUI>();
        }
        private void OnEnable()
        {
            ScreenCaller.ScreenOrientationChanged += ScreenChanged;
        }
        private void OnDisable()
        {
            ScreenCaller.ScreenOrientationChanged -= ScreenChanged;
        }
        public void ScreenChanged(bool isVertical, float aspect)
        {
            if (isVertical)
            {
                an.openPosition = new Vector3(0f, 270f / aspect);
                an.closePosition = new Vector3(0f, 810f / aspect);
                windowContainer.sizeDelta = new Vector2(1080f, 540f / aspect);
            }
            else
            {
                an.openPosition = new Vector3(-270f * aspect, 0f);
                an.closePosition = new Vector3(-810f * aspect, 0f);
                windowContainer.sizeDelta = new Vector2(540f * aspect, 1080f);
            }
            windowContainer.localPosition = an.IsOpen ? an.openPosition : an.closePosition;
        }
        public static void Open(WindowType type, string key)
        {
            self.an.Open();
        }
        public static void Close()
        {
            self.an.Close();
        }
    }

    public enum WindowType : byte
    {
        Static = 0,
        Factory = 1,
        Trading = 2,
    }
}