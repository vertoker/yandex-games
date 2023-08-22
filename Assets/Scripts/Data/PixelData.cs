using System.Collections;
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
        public static int SortingCounter;
        
        private readonly SpriteRenderer _pixel;
        private readonly TextMeshPro _text;
        private PixelDataState _currentState;
        private Coroutine _updater;
        
        public PixelData(SpriteRenderer pixel, TextMeshPro text)
        {
            _text = text;
            _pixel = pixel;
            _currentState = PixelDataState.Select;
            UnSelect();
        }
        
        public void SetColor(Color color, MonoBehaviour processor)
        {
            _pixel.color = color;
            _text.enabled = false;
            _currentState = PixelDataState.Correct;
            _pixel.sortingOrder = SortingCounter;
            SortingCounter++;
            
            if (_updater != null)
                processor.StopCoroutine(_updater);
            _updater = processor.StartCoroutine(CorrectUpdater());
        }

        private IEnumerator CorrectUpdater()
        {
            _pixel.transform.localScale = Extensions.SelectScale;
            var start = Extensions.SelectScale.x;
            var end = Extensions.NormalScale.x;
            for (var i = 0f; i <= 1f; i += Time.deltaTime / Extensions.TimePixelAnim)
            {
                yield return null;
                var scale = Mathf.Lerp(start, end, i);
                _pixel.transform.localScale = new Vector3(scale, scale, scale);
            }
            _pixel.transform.localScale = Extensions.NormalScale;
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
            _text.enabled = true;
            _pixel.sortingOrder = -1;
            _pixel.transform.localScale = Extensions.UnSelectScale;
            _pixel.color = Color.white;
            _currentState = PixelDataState.UnSelect;
        }
    }
}