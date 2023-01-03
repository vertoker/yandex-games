using System;
using System.Collections;
using UnityEngine;

namespace Utility
{
    public class ScreenCaller : MonoBehaviour
    {
        [SerializeField] private float updateTime = 0.2f;
        [SerializeField] private Camera cam;

        private static bool _isInitialized = false;
        private static ScreenOrientation _currentOrientation;
        private static float _screenSizeX, _screenSizeY;
        public static Action<bool, float> ScreenOrientationChanged;
        
        public static bool IsVertical => _currentOrientation is ScreenOrientation.Portrait or ScreenOrientation.PortraitUpsideDown;
        public static float Aspect => _screenSizeX / _screenSizeY;
        public static bool IsInitialized => _isInitialized;
        
        private void Start()
        {
            _currentOrientation = Screen.orientation;
            _screenSizeX = Screen.width;
            _screenSizeY = Screen.height;
            
            Debug.Log("Flip to " + _currentOrientation);
            ScreenOrientationChanged?.Invoke(IsVertical, Aspect);
            _isInitialized = true;

            StartCoroutine(CheckForChange());
        }

        IEnumerator CheckForChange()
        {
            while (true)
            {
                yield return new WaitForSeconds(updateTime);
                if (_currentOrientation != Screen.orientation || _screenSizeX != Screen.width || _screenSizeY != Screen.height)
                {
                    _currentOrientation = Screen.orientation;
                    _screenSizeX = Screen.width;
                    _screenSizeY = Screen.height;
                    
                    Debug.Log("Flip to " + _currentOrientation);
                    ScreenOrientationChanged?.Invoke(IsVertical, Aspect);
                }
            }
        }
    }
}