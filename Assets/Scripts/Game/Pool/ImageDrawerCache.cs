using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Game.Pool
{
    public class ImageDrawerCache : IDisposable
    {
        private static readonly Vector2 PivotCenter = new(0.5f, 0.5f);

        private List<Color> _colors;
        private Texture2D _textureSource;

        public Sprite Sprite { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Stack<PixelActionData> History { get; private set; }

        public Color GetSourcePixel(int x, int y) => _textureSource.GetPixel(x, y);
        public Color GetResultPixel(int x, int y) => Sprite.texture.GetPixel(x, y);

        public void Init(Texture2D texture, List<Color> colors)
        {
            _colors = colors;
            _textureSource = texture;
            Width = texture.width;
            Height = texture.height;
            var textureCache = new Texture2D(Width, Height)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Point
            };
            History = new Stack<PixelActionData>(Width * Height);

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
            Sprite = Sprite.Create(textureCache, rect, PivotCenter, 10);
        }
        public void Dispose()
        {
            _textureSource = null;
            Sprite = null;
        }

        public void SetColor(int x, int y, Color color)
        {
            Sprite.texture.SetPixel(x, y, color);
            Sprite.texture.Apply();
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