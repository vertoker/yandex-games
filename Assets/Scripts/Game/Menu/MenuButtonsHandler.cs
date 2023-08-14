using System;
using Game.Drawer;
using Preset;
using UnityEngine;

namespace Game.Menu
{
    public class MenuButtonsHandler : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] protected int verticalCount;
        [SerializeField] protected float startWidth;
        [SerializeField] protected float blockWidth;
        [SerializeField] protected RectTransform parent;
        [Header("Controller")]
        [SerializeField] protected DrawerController drawerController;
        [SerializeField] protected GameUIController gameUIController;
        [Header("Presets")]
        [SerializeField] protected MenuButton buttonTemplate;
        [SerializeField] protected ImageGroupPreset presets;

        private void Awake()
        {
            var blockCount = Mathf.Floor(presets.Presets.Length / (float)verticalCount);
            parent.sizeDelta = new Vector2(startWidth + blockCount * blockWidth, parent.sizeDelta.y);
            
            foreach (var preset in presets.Presets)
            {
                var button = Instantiate(buttonTemplate, parent);
                button.Install(preset, gameUIController, drawerController);
            }
        }
    }
}