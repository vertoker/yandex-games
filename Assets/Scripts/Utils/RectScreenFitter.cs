using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Scripts.Utils
{
    public class RectScreenFitter : MonoBehaviour
    {
        [SerializeField] private bool width = true;
        [SerializeField] private bool heigth = true;
        [SerializeField] private RectTransform tr;

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
                tr.sizeDelta = new Vector2(width ? 1080f : tr.sizeDelta.x, heigth ? 1080f / aspect : tr.sizeDelta.y);
            }
            else
            {
                tr.sizeDelta = new Vector2(width ? 1080f * aspect : tr.sizeDelta.x, heigth ? 1080f : tr.sizeDelta.y);
            }
        }
    }
}