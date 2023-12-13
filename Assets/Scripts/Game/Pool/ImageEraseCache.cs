using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Game.Pool
{
    public class ImageEraseCache : IDisposable
    {
        private static readonly Vector2 PivotCenter = new(0.5f, 0.5f);
        
        public Sprite Sprite { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        public void Init(Texture2D texture, Color fillColor)
        {
            Width = texture.width;
            Height = texture.height;
            var textureCache = new Texture2D(Width, Height)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Point
            };

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    textureCache.SetPixel(x, y, texture.GetPixel(x, y).a > 0 ? fillColor : SpriteExtensions.TransparentColor);
                }
            }
            textureCache.Apply();

            var rect = new Rect(0, 0, Width, Height);
            Sprite = Sprite.Create(textureCache, rect, PivotCenter, 10);
        }
        public void Dispose()
        {
            Sprite = null;
        }

        public void Erase(int x, int y)
        {
            Sprite.texture.SetPixel(x, y, SpriteExtensions.TransparentColor);
            Sprite.texture.Apply();
        }
    }
}