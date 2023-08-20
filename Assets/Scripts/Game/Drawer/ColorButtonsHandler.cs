using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Drawer
{
    public class ColorButtonsHandler : MonoBehaviour
    {
        [SerializeField] private int poolInit = 30;
        [SerializeField] private ColorButton preset;
        [SerializeField] private Transform parent;

        public event Action<int, ColorButton> SelectedColor;
        public ColorButton Selected => _active;
        
        private Pool<ColorButton> _pool;
        private ColorButton _active;

        private void Awake()
        {
            _pool = new Pool<ColorButton>(preset, parent, poolInit);
        }

        public void SetupColors(Dictionary<Color, List<PixelData>> colors)
        {
            var counter = 0;
            foreach (var pair in colors)
            {
                var data = _pool.Dequeue();
                var index = counter;
                counter++;
                data.Init(pair.Key, pair.Value.Count, counter, () => Switch(index));
            }
            Switch(0);
        }
        public void OnDisable()
        {
            _pool.EnqueueAll();
            _active = null;
        }

        public void SwitchToActive()
        {
            var buttons = _pool.Actives;
            for (var i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].IsFinished)
                    continue;
                Switch(i);
                return;
            }
            if (_active != null)
                _active.Deselect();
        }
        public void Switch(int index)
        {
            if (_active != null)
                _active.Deselect();
            _active = _pool.Actives[index];
            _active.Select();
            SelectedColor?.Invoke(index, _active);
        }
    }
}