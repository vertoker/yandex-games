using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Game.Pool
{
    public class ImageDrawerCache
    {
        private static readonly Vector2 PivotCenter = new(0.5f, 0.5f);

        private Stack<PixelActionData> _history;
        private List<Color> _colors;

        private int _width, _height;
        private Texture2D _textureSource;
        private Sprite _spriteCache;
        
        public Sprite Sprite => _spriteCache;
        public int Width => _width;
        public int Height => _height;
        public Stack<PixelActionData> History => _history;

        public Color GetSourcePixel(int x, int y) => _textureSource.GetPixel(x, y);
        public Color GetResultPixel(int x, int y) => _spriteCache.texture.GetPixel(x, y);

        public void Init(Texture2D texture, List<Color> colors)
        {
            _colors = colors;
            _textureSource = texture;
            _width = texture.width;
            _height = texture.height;
            var textureCache = new Texture2D(Width, Height)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Point
            };
            _history = new Stack<PixelActionData>(Width * Height);

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var gray = texture.GetPixel(x, y).ToGrayNonEqual();
                    textureCache.SetPixel(x, y, gray);
                }
            }
            textureCache.Apply();

            var rect = new Rect(0, 0, Width, Height);
            _spriteCache = Sprite.Create(textureCache, rect, PivotCenter, 10);
        }
        public void Dispose()
        {
            _textureSource = null;
            _spriteCache = null;
        }

        public void SetColor(int x, int y, Color color)
        {
            _spriteCache.texture.SetPixel(x, y, color);
            _spriteCache.texture.Apply();
        }
        public void PushToHistory(int x, int y, Color color)
        {
            History.Push(new PixelActionData
            {
                X = x, Y = y,
                ColorIndex = _colors.IndexOf(color)
            });
        }
    }
}