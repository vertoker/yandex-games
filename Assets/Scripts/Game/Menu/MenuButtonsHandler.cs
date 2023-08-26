using System;
using System.Collections.Generic;
using Data;
using Game.Drawer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

namespace Game.Menu
{
    public class MenuButtonsHandler : MonoBehaviour
    {
        [Header("Progress")]
        [SerializeField] protected ProgressView view;
        [Header("Controller")]
        [SerializeField] protected int verticalCount;
        [SerializeField] protected float startWidth;
        [SerializeField] protected float blockWidth;
        [SerializeField] protected RectTransform parent;
        [Header("Bar")]
        [SerializeField] protected float scrollLerp = 0.2f;
        [SerializeField] protected float scrollSensitivity = 100;
        [SerializeField] protected Scrollbar bar;
        [Header("Controller")]
        [SerializeField] protected DrawerController drawerController;
        [SerializeField] protected GameUIController gameUIController;
        [Header("Presets")]
        [SerializeField] protected MenuButton buttonTemplate;
        [SerializeField] protected ImageGroupPreset presets;

        private List<MenuButton> _buttons;
        private float _targetValueBar;
        private bool _activeScroll;
        private float _width;
        private int _current;

        private void Awake()
        {
            var length = presets.Presets.Length;
            var blockCountFloat = presets.Presets.Length / (float)verticalCount;
            var blockCount = Mathf.Floor(blockCountFloat);
            if (blockCountFloat - blockCount > 0) blockCount++;
            _width = startWidth + blockCount * blockWidth;
            
            parent.sizeDelta = new Vector2(_width, parent.sizeDelta.y);
            _buttons = new List<MenuButton>(length);

            for (var i = 0; i < length; i++)
            {
                var button = Instantiate(buttonTemplate, parent);
                var levelData = YandexGame.savesData.levels[i];
                var preset = presets.Presets[i];
                var action = new UnityAction(() =>
                {
                    gameUIController.Reset();
                    var save = YandexGame.savesData.Get(preset.ImageTitle);
                    if (save != null)
                        drawerController.Init(preset, levelData, button, UpdateButtonsByStatus, save, button.Result);
                    else
                        drawerController.Init(preset, levelData, button, UpdateButtonsByStatus);
                    gameUIController.OpenGame();
                    Switch(button);
                });
                button.Install(preset, levelData, action);
                _buttons.Add(button);
            }
            view.SetPercent(YandexGame.savesData.overallPoints / (float)presets.OverallMax, 3);
        }

        private void Update()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                if (!_activeScroll)
                    _targetValueBar = bar.value;
                _activeScroll = true;
                _targetValueBar = Mathf.Clamp(_targetValueBar - scroll / _width * scrollSensitivity, 0, 1);
            }

            if (!_activeScroll) return;
            
            bar.value = Mathf.Lerp(bar.value, _targetValueBar, scrollLerp);
            if (Math.Abs(bar.value - _targetValueBar) < 0.01F)
                _activeScroll = false;
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

        private void UpdateButtonsByStatus()
        {
            foreach (var button in _buttons)
                button.UpdateButtonByStatus();
            view.SetPercent(YandexGame.savesData.overallPoints / (float)presets.OverallMax, 3);
        }
    }
}