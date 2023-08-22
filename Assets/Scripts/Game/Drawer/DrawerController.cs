using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Audio;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Data;
using Game.Menu;
using Game.Pool;
using UnityEngine.Serialization;

namespace Game.Drawer
{
    public class DrawerController : MonoBehaviour
    {
        [Header("Prefab Groups")]
        [SerializeField] private GameObject gameGroup;
        [SerializeField] private GameObject finishGroup;
        
        [Header("Views")]
        [SerializeField] private ReCenterButton reCenterView;
        [SerializeField] private ProgressView progressView;
        [SerializeField] private ProgressView errorView;
        private int _pixelsCounter, _pixelsCount, _errorsCount;
        
        [Header("Controllers")]
        [SerializeField] private ColorButtonsHandler buttons;
        [SerializeField] private CameraViewer cameraViewer;
        
        [Header("Renderers")]
        [SerializeField] private SpriteRenderer backgroundRenderer;
        [SerializeField] private SpriteRenderer resultRenderer;
        [SerializeField] [Range(0, 1)] private float minBarToFade = 0.2f;
        [SerializeField] private int poolInit = 1000;
        [SerializeField] private Scrollbar bar;
        
        [Header("Pixels")]
        [SerializeField] private PixelPreset pixelPreset;
        [SerializeField] private SpriteRenderer pixelPrefab;
        [SerializeField] private Transform pixelParent;
        
        [Header("Text")]
        [SerializeField] private TextMeshPro textPrefab;
        [SerializeField] private Transform textParent;
        
        // cache
        private ImageDrawerCache _imageCache;
        private Pool<TextMeshPro> _textPool;
        private Pool<SpriteRenderer> _pixelPool;
        
        // data
        private PixelData[,] _pixels;
        private readonly Dictionary<Color, List<PixelData>> _colors = new(10);
        private readonly List<Color> _colorsList = new(10);
        private Coroutine _history;

        public float MinBarToFade => minBarToFade;
        
        private int _switch;

        private void Awake()
        {
            _pixelPool = new Pool<SpriteRenderer>(pixelPrefab, pixelParent, poolInit);
            _textPool = new Pool<TextMeshPro>(textPrefab, textParent, poolInit);
            _imageCache = new ImageDrawerCache();
        }

        public void Init(ImagePreset preset)
        {
            finishGroup.SetActive(false);
            gameGroup.SetActive(true);
            
            _pixelsCount = _pixelsCounter = _errorsCount = 0;
            progressView.SetPercent(0);
            errorView.SetCount(0);
            
            var texture = preset.ImageSource.texture;
            _pixels = new PixelData[texture.width, texture.height];
            PixelData.SortingCounter = 1;
            
            for (var x = 0; x < texture.width; x++)
                for (var y = 0; y < texture.height; y++) 
                    Analyze(texture, x, y);
            
            ValueChanged(bar.value);
            buttons.SetupColors(_colors);
            
            _imageCache.Init(preset.ImageSource.texture, _colors.Keys.ToList());
            backgroundRenderer.sprite = preset.ImageSource;
            resultRenderer.sprite = _imageCache.Sprite;
            resultRenderer.enabled = true;
            
            var size = pixelPreset.GetSize(texture.width, texture.height);
            cameraViewer.Init(size, texture.width, texture.height);
        }
        public void Dispose()
        {
            _colors.Clear();
            _colorsList.Clear();

            backgroundRenderer.sprite = null;
            resultRenderer.sprite = null;
            
            _imageCache.Dispose();
            _pixelPool.EnqueueAll();
            _textPool.EnqueueAll();

            GC.Collect();
        }

        private void Analyze(Texture2D texture, int x, int y)
        {
            var color = texture.GetPixel(x, y);

            if (color.a == 0) return;
            
            var pos = pixelPreset.GetPos(x, y, texture.width, texture.height);
            
            if (_colors.TryAdd(color, new List<PixelData>(10)))
                _colorsList.Add(color);
            
            var pixel = _pixelPool.Dequeue();
            var text = _textPool.Dequeue();
            
            text.transform.position = pos;
            text.text = (_colorsList.IndexOf(color) + 1).ToString();
            
            pixel.transform.position = pos;
            
            var pixelData = new PixelData(pixel, text);
            _pixels[x, y] = pixelData;
            _colors[color].Add(pixelData);
            _pixelsCount++;
        }
        
        private void OnEnable()
        {
            bar.onValueChanged.AddListener(ValueChanged);
            buttons.SelectedColor += SwitchToColorGraySelect;
        }
        private void OnDisable()
        {
            bar.onValueChanged.RemoveListener(ValueChanged);
            buttons.SelectedColor -= SwitchToColorGraySelect;
        }
        
        private void ValueChanged(float value)
        {
            value = (value - MinBarToFade) * (1 / (1 - MinBarToFade));
            resultRenderer.color = new Color(1, 1, 1, value);
        }

        public void GetPixelByPos(Vector2 pos, out int x, out int y)
        {
            pos /= pixelPreset.GridOffset;
            var minX = _imageCache.Width / -2f;
            var minY = _imageCache.Height / -2f;
            var absX = Mathf.Abs(pos.x - minX);
            var absY = Mathf.Abs(pos.y - minY);
            x = Mathf.FloorToInt(absX);
            y = Mathf.FloorToInt(absY);
        }
        public bool CanDrawInit(int x, int y)
        {
            if (!InBounds(x, 0, _imageCache.Width)) return false;
            if (!InBounds(y, 0, _imageCache.Height)) return false;
            if (_pixels[x, y] == null) return false;
            
            var source = _imageCache.GetSourcePixel(x, y);
            var result = _imageCache.GetResultPixel(x, y);
            var draw = buttons.Selected.Color;

            return source == draw && result != draw;
        }
        public bool CanDraw(int x, int y)
        {
            if (!InBounds(x, 0, _imageCache.Width)) return false;
            if (!InBounds(y, 0, _imageCache.Height)) return false;
            if (_pixels[x, y] == null) return false;
            
            var source = _imageCache.GetSourcePixel(x, y);
            var result = _imageCache.GetResultPixel(x, y);
            var draw = buttons.Selected.Color;

            return result != draw && result != source;
        }
        public void DrawPixel(int x, int y)
        {
            var button = buttons.Selected;
            var pixelData = _pixels[x, y];
            
            var source = _imageCache.GetSourcePixel(x, y);
            var draw = button.Color;
            _imageCache.SetColor(x, y, draw);
            
            if (draw == source)
            {
                _pixelsCounter++;
                pixelData.SetColor(draw, this);
                _imageCache.PushToHistory(x, y, draw);
                progressView.SetPercent(_pixelsCounter / (float)_pixelsCount);
                AudioController.Play("pixel");
                button.Add();
                
                if (!button.IsFinished) return;
                
                button.Finish();
                buttons.SwitchToActive();

                if (_pixelsCount != _pixelsCounter) return;
                
                gameGroup.SetActive(false);
                finishGroup.SetActive(true);
                resultRenderer.enabled = false;
                reCenterView.CancelAnim();
                StartCoroutine(reCenterView.ScrollAnim(bar.value, 1));
                var time = reCenterView.TimeScale * (1 - bar.value);
                AudioController.Play("success");
                Invoke(nameof(RepeatHistory), time);//cringe
            }
            else
            {
                pixelData.SetWrongColor(draw);
                _errorsCount++;
                errorView.SetCount(_errorsCount);
            }
        }
        
        private void SwitchToColorGraySelect(int index, ColorButton button)
        {
            var list = _colors[_colorsList[_switch]];
            if (list != null)
                foreach (var pixel in list)
                    pixel.UnSelect();
            
            _switch = index;
            
            list = _colors[_colorsList[_switch]];
            if (list == null) return;
            foreach (var pixel in list)
                pixel.Select();
        }

        public void RepeatHistory()
        {
            if (_history != null)
                StopCoroutine(_history);

            PixelData.SortingCounter = 1;
            var width = _pixels.GetLength(0);
            var height = _pixels.GetLength(1);
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    _pixels[x, y]?.UnSelectPush();
            _history = StartCoroutine(HistoryTimer());
        }

        private IEnumerator HistoryTimer()
        {
            foreach (var data in _imageCache.History.Reverse())
            {
                _pixels[data.X, data.Y].SetColor(_colorsList[data.ColorIndex], this);
                yield return null;
            }
        }

        private static bool InBounds(int value, int min, int max) => min <= value && value < max;
    }
}