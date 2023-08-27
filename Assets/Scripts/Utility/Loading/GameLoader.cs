using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Utility.Loading
{
    public class GameLoader : MonoBehaviour
    {
        [Range(0, 5)] [SerializeField] private float startDelay = 0;
        [SerializeField] private string sceneName = "Game";
        [SerializeField] private AudioController audioController;

        private Action _gameLoaded;

        public event Action OnGameLoaded
        {
            add => _gameLoaded += value;
            remove => _gameLoaded -= value;
        } 

        private int _waitTasks = Application.isEditor ? 3 : 3;
        
        private void OnEnable()
        {
            YandexGame.GetDataEvent += LoadedData;
            audioController.AudioLoaded += LoadedData;
            StartCoroutine(WaitDelay());
        }
        private void OnDisable()
        {
            YandexGame.GetDataEvent -= LoadedData;
            audioController.AudioLoaded -= LoadedData;
        }

        private IEnumerator WaitDelay()
        {
            yield return new WaitForSeconds(startDelay);
            LoadedData();
        }

        private void LoadedData()
        {
            _waitTasks--;
            if (_waitTasks == 0)
                LoadGame();
        }

        private void LoadGame()
        {
            var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            operation.completed += Loaded;
        }

        private void Loaded(AsyncOperation operation)
        {
            _gameLoaded?.Invoke();
        }
    }
}
