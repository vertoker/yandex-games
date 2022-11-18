using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace Scripts
{
    public class ScreenCaller : MonoBehaviour
    {
        [SerializeField] private float updateTime = 0.25f;
        [SerializeField] private Camera cam;
        private ScreenOrientation currentOrientation;

        private static UnityEvent<bool, float> screenOrientationEvent = new UnityEvent<bool, float>();

        public static event UnityAction<bool, float> ScreenOrientationChanged
        {
            add => screenOrientationEvent.AddListener(value);
            remove => screenOrientationEvent.RemoveListener(value);
        }

        private void Start()
        {
            currentOrientation = Screen.orientation;
            Debug.Log("Flip to " + currentOrientation.ToString());
            var isVertical = currentOrientation == ScreenOrientation.Portrait || currentOrientation == ScreenOrientation.PortraitUpsideDown;
            cam.orthographicSize = isVertical ? 5 / cam.aspect : 5;
            screenOrientationEvent.Invoke(isVertical, cam.aspect);

            StartCoroutine(CheckForChange());
        }

        IEnumerator CheckForChange()
        {
            while (true)
            {
                yield return new WaitForSeconds(updateTime);
                if (currentOrientation != Screen.orientation)
                {
                    currentOrientation = Screen.orientation;
                    Debug.Log("Flip to " + currentOrientation.ToString());
                    var isVertical = currentOrientation == ScreenOrientation.Portrait || currentOrientation == ScreenOrientation.PortraitUpsideDown;
                    cam.orthographicSize = isVertical ? 5 / cam.aspect : 5;
                    screenOrientationEvent.Invoke(isVertical, cam.aspect);
                }
            }
        }
    }
}