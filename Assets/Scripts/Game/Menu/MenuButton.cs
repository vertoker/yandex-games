using System;
using System.Linq;
using Data;
using Game.Drawer;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Game.Menu
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text score;
        [SerializeField] private RawImage image;
        [SerializeField] private Button button;

        private ImagePreset _preset;
        private LevelData _data;
        
        public Texture2D Result { get; private set; }
        
        public void Install(ImagePreset preset, LevelData data, UnityAction action)
        {
            _preset = preset;
            _data = data;
            UpdateData();
            
            button.onClick.AddListener(action);
        }

        public void UpdateData()
        {
            var source = _preset.ImageSource.texture;
            var save = YandexGame.savesData.Get(_preset.ImageTitle);
            var aspect = source.width / (float)source.height;
            var size = image.rectTransform.sizeDelta.y;
            image.rectTransform.sizeDelta = aspect < 1 
                ? new Vector2(size * aspect, size) 
                : new Vector2(size, size / aspect);
            
            if (save != null)
            {
                Result = new Texture2D(source.width, source.height)
                {
                    wrapMode = TextureWrapMode.Clamp,
                    filterMode = FilterMode.Point
                };
                save.ReadData(source, Result);
                
                image.texture = Result;
                score.text = _data.GetNormalizedScore().ToString();
            }
            else if (_data.completed)
            {
                image.texture = source;
                score.text = _data.GetNormalizedScore().ToString();
            }
            else
            {
                Result = new Texture2D(source.width, source.height)
                {
                    wrapMode = TextureWrapMode.Clamp,
                    filterMode = FilterMode.Point
                };
                var pixels = source.GetPixels();
                var length = source.width * source.height;
                for (var i = 0; i < length; i++)
                    pixels[i] = pixels[i].ToGrayNonEqual();
                Result.SetPixels(pixels);
                Result.Apply();
                
                image.texture = Result;
                score.text = string.Empty;
            }
        }

        public void Click()
        {
            button.onClick.Invoke();
        }
    }
}