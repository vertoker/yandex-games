using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Data
{
    public class ColorButton : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Button colorPicker;
        [Space]
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image backgroundProgress;
        [SerializeField] private Image progress;
        [SerializeField] private Image check;

        private Color _selectedColor;
        private int _count;
        private int _counter;
        
        private float GetProgress() => _counter / (float)_count;
        public bool IsFinished => _counter == _count;

        public void Init(Color color, int count, int indexColor)
        {
            _selectedColor = color;
            _count = count;
            _counter = 0;

            background.color = _selectedColor;
            //colorPicker
            
            text.enabled = true;
            text.text = indexColor.ToString();
            backgroundProgress.color = _selectedColor;
            backgroundProgress.enabled = false;
            progress.enabled = false;
            check.enabled = false;
        }

        public void Select()
        {
            text.fontStyle = FontStyles.Bold;
            backgroundProgress.enabled = true;
            progress.enabled = true;
        }
        public void Deselect()
        {
            text.fontStyle = FontStyles.Normal;
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
            text.enabled = false;
            backgroundProgress.enabled = false;
            progress.enabled = false;
        }
    }
}