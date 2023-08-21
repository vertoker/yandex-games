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
        public ColorButton Selected { get; private set; }

        private Pool<ColorButton> _pool;

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
            Selected = null;
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
            if (Selected != null)
                Selected.Deselect();
        }
        public void Switch(int index)
        {
            if (Selected != null)
                Selected.Deselect();
            Selected = _pool.Actives[index];
            Selected.Select();
            SelectedColor?.Invoke(index, Selected);
        }
    }
}