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
    public enum ButtonStatus
    {
        Locked,
        Unlocked,
        InProgress,
        Completed
    }
    
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private TMP_Text score;
        [SerializeField] private RawImage image;
        [SerializeField] private Button button;
        [SerializeField] private Image success;

        private ButtonStatus _status;
        private ImagePreset _preset;
        private LevelData _data;
        
        public Texture2D Result { get; private set; }
        
        public void Install(ImagePreset preset, LevelData data, UnityAction action)
        {
            _preset = preset;
            _data = data;
            UpdateButtonConfigure();
            
            button.onClick.AddListener(action);
        }

        public void UpdateButtonByStatus()
        {
            if (_status != ButtonStatus.Locked) return;
            var source = _preset.ImageSource.texture;
            SetStatusUnlocked(source);
        }
        
        public void UpdateButtonConfigure()
        {
            var source = _preset.ImageSource.texture;
            var save = YandexGame.savesData.Get(_preset.ImageTitle);
            var aspect = source.width / (float)source.height;
            var size = image.rectTransform.sizeDelta.y;
            image.rectTransform.sizeDelta = aspect < 1 
                ? new Vector2(size * aspect, size) 
                : new Vector2(size, size / aspect);

            if (_preset.LevelActiveThreshold > YandexGame.savesData.overallPoints)
                SetStatusLocked(source);
            else if (save != null)
                SetStatusInProgress(source, save);
            else if (_data.completed)
                SetStatusCompleted(source);
            else
                SetStatusUnlocked(source);

            if (_data.maxPoints > _preset.PixelsCount)
                _data.maxPoints = _preset.PixelsCount;
            success.enabled = _data.completed;
            background.color = _data.maxPoints == _preset.PixelsCount ? _preset.SuccessColor : Color.white;
        }
        
        private void SetStatusLocked(Texture2D source)
        {
            _status = ButtonStatus.Locked;
            image.texture = source;
            image.color = Color.black;
            button.interactable = false;
            score.text = _preset.LevelActiveThreshold.ToString();
        }
        private void SetStatusInProgress(Texture2D source, ImageHistorySerialization save)
        {
            _status = ButtonStatus.InProgress;
            Result = new Texture2D(source.width, source.height)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Point
            };
            save.ReadData(source, Result);

            image.texture = Result;
            image.color = Color.white;
            button.interactable = true;
            score.text = _data.maxPoints.ToString();
        }
        private void SetStatusCompleted(Texture2D source)
        {
            _status = ButtonStatus.Completed;
            image.texture = source;
            image.color = Color.white;
            button.interactable = true;
            score.text = _data.maxPoints.ToString();
        }
        private void SetStatusUnlocked(Texture2D source)
        {
            _status = ButtonStatus.Unlocked;
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
            image.color = Color.white;
            button.interactable = true;
            score.text = string.Empty;
        }


        public void Click()
        {
            button.onClick.Invoke();
        }
    }
}