using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Presets/" + nameof(ImagePreset), fileName = nameof(ImagePreset))]
    public class ImagePreset : ScriptableObject
    {
        [SerializeField] private Sprite imageSource;
        [SerializeField] private int levelActiveThreshold;
        [Header("Generated")]
        [SerializeField] private string imageTitle;
        [SerializeField] private Color successColor = Color.white;
        [SerializeField] private int pixelsCount;
        [Header("Editor")]
        [SerializeField] private float percentComplete = 0.8f;

        public void OnValidate()
        {
            if (ImageSource == null) return;
            if (ImageSource.texture == null) return;
            
            var pixels = ImageSource.texture.GetPixels();
            var length = pixels.Length;
            pixelsCount = 0;

            var colors = new Dictionary<Color, int>();
            for (var i = 0; i < length; i++)
            {
                if (!colors.ContainsKey(pixels[i]))
                    colors.Add(pixels[i], 0);
                colors[pixels[i]]++;
                
                if (pixels[i].a >= 0.5f)
                    pixelsCount++;
            }
            
            successColor = colors
                .OrderByDescending(c => c.Value)
                .FirstOrDefault(c => c.Key.GetPower() > 0.5f
                                     && c.Key.GetPower() < 0.9f
                                     && c.Key.a > 0.9f).Key;
            
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public Color SuccessColor => successColor;
        

        public string ImageTitle
        {
            get => imageTitle;
            set => imageTitle = value;
        }
        public Sprite ImageSource
        {
            get => imageSource;
            set => imageSource = value;
        }

        public int PixelCountWithPercent => (int)(PixelsCount * percentComplete);

        public int LevelActiveThreshold
        {
            get => levelActiveThreshold;
            set => levelActiveThreshold = value;
        }

        public int PixelsCount => pixelsCount;
    }
}