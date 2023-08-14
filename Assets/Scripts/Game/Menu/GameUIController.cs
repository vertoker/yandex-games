using Core.UI;
using UnityEngine;

namespace Game.Menu
{
    [RequireComponent(typeof(CanvasController))]
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private GameObject initWindow;
        private CanvasController _controller;

        private void Start()
        {
            _controller = GetComponent<CanvasController>();
            _controller.CloseAll();
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
    }
}