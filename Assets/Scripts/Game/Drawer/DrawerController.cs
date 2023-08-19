using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Data;
using Game.Pool;

namespace Game.Drawer
{
    public class DrawerController : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] private ColorButtonsHandler buttons;
        [SerializeField] private CameraViewer cameraViewer;
        
        [Header("Renderers")]
        [SerializeField] private SpriteRenderer background;
        [SerializeField] private SpriteRenderer result;
        [SerializeField] private int poolInit = 1000;
        [SerializeField] private Scrollbar bar;
        
        [Header("Pixels")]
        [SerializeField] private PixelPreset pixelPreset;
        [SerializeField] private SpriteRenderer pixelPrefab;
        [SerializeField] private Transform pixelParent;
        
        private ImageDrawerCache _imageCache;
        private Pool<SpriteRenderer> _pixelPool;
        
        [Header("Text")]
        [SerializeField] private TextMeshPro textPrefab;
        [SerializeField] private Transform textParent;
        private Pool<TextMeshPro> _textPool;

        private readonly Dictionary<Color, int> _colors = new(10);
        private readonly List<Color> _colorsList = new(10);

        private void Awake()
        {
            _pixelPool = new Pool<SpriteRenderer>(pixelPrefab, pixelParent, poolInit);
            _textPool = new Pool<TextMeshPro>(textPrefab, textParent, poolInit);
            _imageCache = new ImageDrawerCache();
        }

        public void Init(ImagePreset preset)
        {
            var texture = preset.ImageSource.texture;
            for (var x = 0; x < texture.width; x++)
            {
                for (var y = 0; y < texture.height; y++)
                {
                    Analyze(texture, x, y);
                }
            }
            
            ValueChanged(bar.value);
            buttons.SetupColors(_colors);
            
            _imageCache.Init(preset.ImageSource.texture, _colors);
            background.sprite = preset.ImageSource;
            result.sprite = _imageCache.Sprite;
            
            var size = pixelPreset.GetSize(texture.width, texture.height);
            cameraViewer.Init(size);
        }
        public void Dispose()
        {
            _colors.Clear();
            _colorsList.Clear();

            background.sprite = null;
            result.sprite = null;
            
            _imageCache.Dispose();
            _pixelPool.EnqueueAll();
            _textPool.EnqueueAll();
        }

        private void Analyze(Texture2D texture, int x, int y)
        {
            var color = texture.GetPixel(x, y);

            if (color.a == 0) return;
            
            var pos = pixelPreset.GetPos(x, y, texture.width, texture.height);
            
            if (_colors.TryAdd(color, 0))
                _colorsList.Add(color);
            _colors[color]++;
            
            var pixel = _pixelPool.Dequeue();
            var text = _textPool.Dequeue();
            
            text.transform.position = pos;
            text.text = (_colorsList.IndexOf(color) + 1).ToString();
            
            pixel.transform.position = pos;
            //pixel.color = color;
        }
        
        private void OnEnable()
        {
            bar.onValueChanged.AddListener(ValueChanged);
        }
        private void OnDisable()
        {
            bar.onValueChanged.RemoveListener(ValueChanged);
        }
        
        private void ValueChanged(float value)
        {
            result.color = new Color(1, 1, 1, value);
        }
    }
}