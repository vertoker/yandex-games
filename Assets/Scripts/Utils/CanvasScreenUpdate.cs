using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Scripts.Utils
{
    public class CanvasScreenUpdate : MonoBehaviour
    {
        [SerializeField] private CanvasScaler canvas;

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
                canvas.matchWidthOrHeight = 0f;
                canvas.referenceResolution = new Vector2(1080f, 1080f / aspect);
            }
            else
            {
                canvas.matchWidthOrHeight = 0.5f;
                canvas.referenceResolution = new Vector2(1080f * aspect, 1080f);
            }
        }
    }
}