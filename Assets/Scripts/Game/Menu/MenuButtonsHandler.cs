using System;
using System.Collections.Generic;
using Data;
using Game.Drawer;
using UnityEngine;
using UnityEngine.Events;
using YG;

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
            var length = presets.Presets.Length;
            var blockCount = Mathf.Floor(presets.Presets.Length / (float)verticalCount);
            parent.sizeDelta = new Vector2(startWidth + blockCount * blockWidth, parent.sizeDelta.y);
            _buttons = new List<MenuButton>(length);

            for (var i = 0; i < length; i++)
            {
                var button = Instantiate(buttonTemplate, parent);
                var levelData = YandexGame.savesData.levels[i];
                var preset = presets.Presets[i];
                var action = new UnityAction(() =>
                {
                    var index = i;
                    gameUIController.Reset();
                    drawerController.Init(preset, levelData);
                    gameUIController.OpenGame();
                    Switch(button);
                });
                button.Install(preset, levelData, action);
                _buttons.Add(button);
            }
        }

        public void Restart()
        {
            _buttons[_current].Click();
        }
        public void SwitchNext()
        {
            _current++;
            if (_current == _buttons.Count)
                _current = 0;
            Restart();
        }
        private void Switch(MenuButton active)
        {
            _current = _buttons.IndexOf(active);
        }
    }
}