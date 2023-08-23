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
        
        public void Install(ImagePreset preset, LevelData data, UnityAction action)
        {
            var source = preset.ImageSource.texture;
            var save = YandexGame.savesData.tempSaves
                .FirstOrDefault(s => s.Key == preset.ImageTitle);
            var aspect = source.width / (float)source.height;
            var size = image.rectTransform.sizeDelta.y;
            image.rectTransform.sizeDelta = aspect < 1 
                ? new Vector2(size * aspect, size) 
                : new Vector2(size, size / aspect);
            
            if (save != null)
            {
                var result = new Texture2D(source.width, source.height)
                {
                    wrapMode = TextureWrapMode.Clamp,
                    filterMode = FilterMode.Point
                };
                save.ReadData(source, result);
                
                image.texture = result;
                score.text = $"{data.points - data.errors} / {data.points}";
            }
            else if (data.completed)
            {
                image.texture = source;
                score.text = $"{data.points - data.errors} / {data.points}";
            }
            else
            {
                var result = new Texture2D(source.width, source.height)
                {
                    wrapMode = TextureWrapMode.Clamp,
                    filterMode = FilterMode.Point
                };
                var pixels = source.GetPixels();
                var length = source.width * source.height;
                for (var i = 0; i < length; i++)
                    pixels[i] = pixels[i].ToGrayNonEqual();
                result.SetPixels(pixels);
                result.Apply();
                
                image.texture = result;
                score.text = string.Empty;
            }
            button.onClick.AddListener(action);
        }

        public void Click()
        {
            button.onClick.Invoke();
        }
    }
}