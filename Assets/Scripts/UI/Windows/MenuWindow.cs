using System;
using Camera;
using Camera.Watchers;
using UnityEngine;

namespace UI.Windows
{
    public class MenuWindow : BaseWindow
    {
        [SerializeField] private MenuWatcher menuWatcher;

        public override void Enable()
        {
            CameraController.Switch(menuWatcher);
        }
    }
}