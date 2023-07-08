using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class CanvasController : MonoBehaviour
    {
        private Dictionary<string, GameObject> _windows;
        private GameObject _activeWindow;

        private void Awake()
        {
            var length = transform.childCount;
            _windows = new Dictionary<string, GameObject>(length);
            for (var i = 0; i < length; i++)
            {
                var window = transform.GetChild(i).gameObject;
                _windows.Add(ToName(window), window);
            }
        }
        
        public void Open(string key)
        {
            CloseActive();
            
            _activeWindow = _windows[key.ToLower()];
            if (_activeWindow != null)
                _activeWindow.SetActive(true);
        }
        public void Close(string key)
        {
            _activeWindow = _windows[key.ToLower()];
            if (_activeWindow != null)
                _activeWindow.SetActive(false);
        }
        public void CloseActive()
        {
            if (_activeWindow == null) return;
            _activeWindow.SetActive(false);
            _activeWindow = null;
        }
        public void CloseAll()
        {
            foreach (var window in _windows)
                window.Value.SetActive(false);
            _activeWindow = null;
        }

        private static string ToName(GameObject window)
        {
            return window.name.ToLower();
        }
    }
}
