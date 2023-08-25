using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(menuName = "Presets/" + nameof(ImagePreset), fileName = nameof(ImagePreset))]
    public class ImagePreset : ScriptableObject
    {
        [SerializeField] private string imageTitle;
        [SerializeField] private Sprite imageSource;
        [SerializeField] private Color successColor = Color.white;
        [SerializeField] private int pixelsCount;

        public void OnValidate()
        {
            var pixels = imageSource.texture.GetPixels();
            var length = pixels.Length;
            pixelsCount = 0;
            for (var i = 0; i < length; i++)
            {
                if (pixels[i].a >= 0.5f)
                {
                    pixelsCount = PixelsCount + 1;
                }
            }
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public string ImageTitle => imageTitle;
        public Sprite ImageSource => imageSource;
        public Color SuccessColor => successColor;
        public int PixelsCount => pixelsCount;
    }
}