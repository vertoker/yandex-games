using System;
using System.Linq;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Presets/" + nameof(ImageGroupPreset), fileName = nameof(ImageGroupPreset))]
    public class ImageGroupPreset : ScriptableObject
    {
        [SerializeField] private int overallMax;
        [SerializeField] private ImagePreset[] presets;
        [Header("Editor")]
        [SerializeField] private int freeLevels;

        public void OnValidate()
        {
            overallMax = (from preset in Presets select preset.PixelsCount).Sum();
        }
        
        public int OverallMax => overallMax;

        public ImagePreset[] Presets
        {
            get => presets;
            set => presets = value;
        }
        
        public int FreeLevels => freeLevels;
    }
}