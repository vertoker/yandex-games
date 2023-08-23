using System;
using Core.UI;
using UnityEngine;
using YG;

namespace Game.Menu
{
    [RequireComponent(typeof(CanvasController))]
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private GameObject initWindow;
        private CanvasController _controller;

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= Open;
        }

        private void Start()
        {
            _controller = GetComponent<CanvasController>();
            if (YandexGame.initializedLB)
            {
                Open();
            }
            else
            {
                YandexGame.GetDataEvent += Open;
            }
        }

        private void Open()
        {
            if (initWindow != null)
                _controller.Open(initWindow.name);
        }

        public void OpenMenu()
        {
            _controller.Open("Menu");
        }
        public void OpenGame()
        {
            _controller.Open("Game");
        }
        public void Reset()
        {
            _controller.CloseAll();
        }
    }
}