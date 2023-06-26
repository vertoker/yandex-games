using System;
using Core.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Core
{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField] private string sceneName = "Game";
        [SerializeField] private AudioController audioController;

        private int waitTasks = 2;
        
        private void OnEnable()
        {
            YandexGame.GetDataEvent += LoadedData;
            audioController.AudioLoaded += LoadedData;
        }
        private void OnDisable()
        {
            YandexGame.GetDataEvent -= LoadedData;
            audioController.AudioLoaded -= LoadedData;
        }

        private void LoadedData()
        {
            waitTasks--;
            if (waitTasks == 0)
                LoadGame();
        }

        private void LoadGame()
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
    }
}
