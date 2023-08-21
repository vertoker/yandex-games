using System;
using System.Collections.Generic;
using Data;
using Game.Drawer;
using UnityEngine;
using UnityEngine.Events;

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

        private List<MenuButton> _buttons;
        private int _current;

        private void Awake()
        {
            var blockCount = Mathf.Floor(presets.Presets.Length / (float)verticalCount);
            parent.sizeDelta = new Vector2(startWidth + blockCount * blockWidth, parent.sizeDelta.y);
            _buttons = new List<MenuButton>(presets.Presets.Length);
            
            foreach (var preset in presets.Presets)
            {
                var button = Instantiate(buttonTemplate, parent);
                var action = new UnityAction(() =>
                {
                    gameUIController.Reset();
                    drawerController.Init(preset);
                    gameUIController.OpenGame();
                    Switch(button);
                });
                button.Install(preset, action);
                _buttons.Add(button);
            }
        }

        public void SwitchNext()
        {
            _current++;
            if (_current == presets.Presets.Length)
                _current = 0;
            _buttons[_current].Click();
        }
        private void Switch(MenuButton active)
        {
            _current = _buttons.IndexOf(active);
        }
    }
}