using Preset;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Game.Menu
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private Image image;
        
        public void Install(ImagePreset preset)
        {
            title.text = preset.ImageTitle;
            image.sprite = preset.ImageSource;
        }
    }
}