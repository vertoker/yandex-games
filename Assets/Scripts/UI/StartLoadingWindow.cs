using UnityEngine;
using Utility.Loading;

namespace UI
{
    [RequireComponent(typeof(CanvasController))]
    public class StartLoadingWindow : MonoBehaviour
    {
        [SerializeField] private GameObject loadingWindow;
        [SerializeField] private GameObject startWindow;
        [SerializeField] private GameLoader loader;
        private CanvasController _controller;

        private void Awake()
        {
            _controller = GetComponent<CanvasController>();
            if (loadingWindow != null)
                _controller.Open(loadingWindow.name);
        }

        private void OnEnable()
        {
            loader.OnGameLoaded += Loaded;
        }
        private void OnDisable()
        {
            loader.OnGameLoaded -= Loaded;
        }

        private void Loaded()
        {
            if (startWindow != null)
                _controller.Open(startWindow.name);
        }
    }
}
