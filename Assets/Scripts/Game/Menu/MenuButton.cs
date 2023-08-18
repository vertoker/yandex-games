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
        
        public void Install(ImagePreset preset, GameUIController controller, DrawerController drawer)
        {
            image.sprite = preset.ImageSource;

            var action = new UnityAction(() =>
            {
                drawer.Init(preset);
                controller.OpenGame();
            });
            button.onClick.AddListener(action);
        }
    }
}