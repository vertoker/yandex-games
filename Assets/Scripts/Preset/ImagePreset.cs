using UnityEngine;
using UnityEngine.Serialization;

namespace Preset
{
    [CreateAssetMenu(menuName = "Presets/" + nameof(ImagePreset), fileName = nameof(ImagePreset))]
    public class ImagePreset : ScriptableObject
    {
        [SerializeField] private string imageTitle;
        [SerializeField] private Sprite imageSource;

        public string ImageTitle => imageTitle;
        public Sprite ImageSource => imageSource;
    }
}