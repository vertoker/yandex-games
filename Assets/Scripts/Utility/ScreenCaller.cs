using System;
using System.Collections;
using UnityEngine;

namespace Utility
{
    public class ScreenCaller : MonoBehaviour
    {
        [SerializeField] private float updateTime = 0.2f;
        [SerializeField] private Camera cam;
        private ScreenOrientation currentOrientation;
        private float screenSizeX, screenSizeY;

        public static Action<bool, float> ScreenOrientationChanged;

        private void Start()
        {
            currentOrientation = Screen.orientation;
            screenSizeX = Screen.width;
            screenSizeY = Screen.height;
            Debug.Log("Flip to " + currentOrientation.ToString());
            var isVertical = currentOrientation == ScreenOrientation.Portrait || currentOrientation == ScreenOrientation.PortraitUpsideDown;
            ScreenOrientationChanged?.Invoke(isVertical, cam.aspect);

            StartCoroutine(CheckForChange());
        }

        IEnumerator CheckForChange()
        {
            while (true)
            {
                yield return new WaitForSeconds(updateTime);
                if (currentOrientation != Screen.orientation || screenSizeX != Screen.width || screenSizeY != Screen.height)
                {
                    currentOrientation = Screen.orientation;
                    screenSizeX = Screen.width;
                    screenSizeY = Screen.height;
                    Debug.Log("Flip to " + currentOrientation.ToString());
                    var isVertical = currentOrientation == ScreenOrientation.Portrait || currentOrientation == ScreenOrientation.PortraitUpsideDown;
                    //cam.orthographicSize = isVertical ? 5 / cam.aspect : 5;
                    ScreenOrientationChanged?.Invoke(isVertical, cam.aspect);
                }
            }
        }
    }
}