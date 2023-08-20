using System;
using Game.Drawer;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Data
{
    public class ColorButton : MonoBehaviour
    {
        [SerializeField] private float disableSize = 50;
        [SerializeField] private float enableSize = 80;
        [SerializeField] private Image background;
        [SerializeField] private Button colorPicker;
        [Space]
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image backgroundProgress;
        [SerializeField] private Image progress;
        [SerializeField] private Image check;

        public Color Color => _selectedColor;
        
        private Color _selectedColor;
        private int _count;
        private int _counter;
        
        private float GetProgress() => _counter / (float)_count;
        public bool IsFinished => _counter == _count;

        public void Init(Color color, int count, int numberColor, UnityAction switchAction)
        {
            _selectedColor = color;
            _count = count;
            _counter = 0;

            background.color = _selectedColor;
            colorPicker.onClick.AddListener(switchAction);
            
            text.enabled = true;
            text.text = numberColor.ToString();
            
            progress.color = color;
            var highlight = color.ToHighlighted();
            backgroundProgress.color = highlight;
            text.color = highlight;
            check.color = highlight;
            
            progress.fillAmount = GetProgress();
            backgroundProgress.enabled = false;
            progress.enabled = false;
            check.enabled = false;
        }
        private void OnDisable()
        {
            colorPicker.onClick.RemoveAllListeners();
        }

        public void Select()
        {
            text.fontSize = enableSize;
            backgroundProgress.enabled = true;
            progress.enabled = true;
        }
        public void Deselect()
        {
            text.fontSize = disableSize;
            backgroundProgress.enabled = false;
            progress.enabled = false;
        }

        public void Add()
        {
            if (IsFinished) return;
            _counter++;

            progress.fillAmount = GetProgress();
        }
        public void Finish()
        {
            colorPicker.onClick.RemoveAllListeners();
            text.enabled = false;
            progress.enabled = false;
            backgroundProgress.enabled = false;
            check.enabled = true;
        }
    }
}