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
        [SerializeField] private Scrollbar bar;
        private RectTransform _content;

        public event Action<int, ColorButton> SelectedColor;
        public ColorButton Selected { get; private set; }
        private int _selectedIndex;

        private Pool<ColorButton> _pool;

        private void Awake()
        {
            _content = parent.GetComponent<RectTransform>();
            _pool = new Pool<ColorButton>(preset, parent, poolInit);
        }

        public void SetupColors(Dictionary<Color, List<PixelData>> colors)
        {
            var counter = 0;
            foreach (var pair in colors)
            {
                var data = _pool.Dequeue();
                var index = counter; counter++;
                data.transform.SetSiblingIndex(index);
                data.Init(pair.Key, pair.Value.Count, counter, () => Switch(index));
            }
            _content.sizeDelta = new Vector2(170f * colors.Count, _content.sizeDelta.y);
            bar.value = 0;
            Switch(0);
        }
        private void OnDisable()
        {
            Dispose();
        }
        public void Dispose()
        {
            _pool.EnqueueAll();
            Deselect();
        }

        private void Deselect()
        {
            if (Selected != null)
                Selected.Deselect();
            Selected = null;
            _selectedIndex = -1;
        }

        public void SwitchToActive()
        {
            var buttons = _pool.Actives;
            for (var i = 0; i < buttons.Count; i++)
            {
                var index = (int)Mathf.Repeat(i + _selectedIndex, buttons.Count);
                if (buttons[index].IsFinished)
                    continue;
                bar.value = index / (float)buttons.Count;
                Switch(index);
                return;
            }
            Deselect();
        }
        public void Switch(int index)
        {
            if (index == _selectedIndex)
                return;
            Deselect();
            
            Selected = _pool.Actives[index];
            _selectedIndex = index;
            
            Selected.Select();
            SelectedColor?.Invoke(index, Selected);
        }
    }
}