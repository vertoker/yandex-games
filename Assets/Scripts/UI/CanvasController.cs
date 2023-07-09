using System.Collections.Generic;
using UI.Windows;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class CanvasController : MonoBehaviour
    {
        private Dictionary<string, BaseWindow> _windows;
        [SerializeField] private BaseWindow activeWindow;

        private void Awake()
        {
            var windows = transform.GetComponentsInChildren<BaseWindow>();
            var length = windows.Length;
            
            _windows = new Dictionary<string, BaseWindow>(length);
            foreach (var window in windows)
            {
                _windows.Add(ToName(window), window);
                window.Init(this);
                window.gameObject.SetActive(false);
            }
            OpenWindow(activeWindow);
        }
        
        public void Open(string key)
        {
            CloseActive();
            activeWindow = _windows[key.ToLower()];
            OpenWindow(activeWindow);
        }
        public void Open(BaseWindow window)
        {
            CloseActive();
            activeWindow = window;
            OpenWindow(activeWindow);
        }
        public void Close(string key)
        {
            activeWindow = _windows[key.ToLower()];
            CloseWindow(activeWindow);
        }
        public void CloseActive()
        {
            CloseWindow(activeWindow);
            activeWindow = null;
        }
        public void CloseAll()
        {
            foreach (var window in _windows)
                window.Value.gameObject.SetActive(false);
            activeWindow = null;
        }

        private static void OpenWindow(BaseWindow window)
        {
            if (window == null) return;
            window.gameObject.SetActive(true);
            window.Enable();
        }
        private static void CloseWindow(BaseWindow window)
        {
            if (window == null) return;
            window.Disable();
            window.gameObject.SetActive(false);
        }
        private static string ToName(BaseWindow window)
        {
            return window.name.ToLower();
        }
    }
}
