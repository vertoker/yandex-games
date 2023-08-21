using TMPro;
using UnityEngine;

namespace Data
{
    public enum PixelDataState
    {
        UnSelect,
        Select,
        Wrong,
        Correct
    }
    
    public class PixelData
    {
        private readonly SpriteRenderer _pixel;
        private readonly TextMeshPro _text;
        private PixelDataState _currentState;
        
        public PixelData(SpriteRenderer pixel, TextMeshPro text)
        {
            _pixel = pixel;
            _text = text;
            _currentState = PixelDataState.Select;
            UnSelect();
        }
        
        public void SetColor(Color color)
        {
            _pixel.color = color;
            _pixel.transform.localScale = Extensions.NormalScale;
            _text.enabled = false;
            _currentState = PixelDataState.Correct;
        }
        public void SetWrongColor(Color color)
        {
            _pixel.color = color.ToNonContrast();
            _currentState = PixelDataState.Wrong;
        }

        public void Select()
        {
            if (_currentState != PixelDataState.UnSelect) return;
            _pixel.color = Extensions.LightGray;
            _currentState = PixelDataState.Select;
        }
        public void UnSelect()
        {
            if (_currentState != PixelDataState.Select) return;
            UnSelectPush();
        }
        public void UnSelectPush()
        {
            _pixel.transform.localScale = Extensions.UnSelectScale;
            _pixel.color = Color.white;
            _currentState = PixelDataState.UnSelect;
        }
    }
}