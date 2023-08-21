using System;
using Data;
using Game.Drawer;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Menu
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Button button;
        
        public void Install(ImagePreset preset, UnityAction action)
        {
            image.sprite = preset.ImageSource;
            button.onClick.AddListener(action);
        }

        public void Click()
        {
            button.onClick.Invoke();
        }
    }
}