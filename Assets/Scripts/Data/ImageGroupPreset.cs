using UnityEngine;

namespace Preset
{
    [CreateAssetMenu(menuName = "Presets/" + nameof(ImageGroupPreset), fileName = nameof(ImageGroupPreset))]
    public class ImageGroupPreset : ScriptableObject
    {
        [SerializeField] private ImagePreset[] presets;

        public ImagePreset[] Presets => presets;
    }
}