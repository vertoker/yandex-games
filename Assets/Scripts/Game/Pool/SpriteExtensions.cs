using UnityEngine;

namespace Game.Pool
{
    public static class SpriteExtensions
    {
        private static readonly Vector2 PivotCenter = new(0.5f, 0.5f);
        public static readonly Color TransparentColor = new(0, 0, 0, 0);
        
        public static Sprite CreateWithFillColor(this Sprite sprite, Color color)
        {
            var texture = sprite.texture;
            var width = texture.width;
            var height = texture.height;
            
            var textureCache = new Texture2D(width, height)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Point
            };
            
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    textureCache.SetPixel(x, y, texture.GetPixel(x, y).a > 0 ? color : TransparentColor);
                }
            }
            textureCache.Apply();
            
            var rect = new Rect(0, 0, width, height);
            return Sprite.Create(textureCache, rect, PivotCenter, 10);
        }
    }
}