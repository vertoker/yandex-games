using System;
using UnityEngine;

namespace Game.Pool
{
    public class ImageDrawerCache
    {
        private static readonly Vector2 PivotCenter = new Vector2(0.5f, 0.5f);
        
        private Texture2D _textureSource;
        private Sprite _spriteCache;
        
        public Sprite Sprite => _spriteCache;
        public event Action<int, int> OnPixelUpdate;
        
        public void Init(Texture2D texture)
        {
            _textureSource = texture;
            var textureCache = new Texture2D(texture.width, texture.height)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Point
            };

            for (var x = 0; x < texture.width; x++)
            {
                for (var y = 0; y < texture.height; y++)
                {
                    var gray = GetGray(texture.GetPixel(x, y));
                    textureCache.SetPixel(x, y, gray);
                }
            }
            textureCache.Apply();

            var rect = new Rect(0, 0, texture.width, texture.height);
            _spriteCache = Sprite.Create(textureCache, rect, PivotCenter, 10);
        }
        public void Dispose()
        {
            _textureSource = null;
            _spriteCache = null;
        }

        public void SetColored(int x, int y)
        {
            var color = _textureSource.GetPixel(x, y);
            _spriteCache.texture.SetPixel(x, y, color);
            _spriteCache.texture.Apply();
            
            OnPixelUpdate?.Invoke(x, y);
        }

        private static Color GetGray(Color pixel)
        {
            var gray = (pixel.r + pixel.g + pixel.b) / 3f;
            return new Color(gray, gray, gray, pixel.a);
        }
    }
}