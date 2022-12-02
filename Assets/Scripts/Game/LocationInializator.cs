using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

namespace Scripts.Game
{
    public class LocationInializator : MonoBehaviour
    {
        public static Action LocationLoaded;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += InitializeLocation;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= InitializeLocation;
        }

        private void InitializeLocation(Scene scene, LoadSceneMode loadMode)
        {
            if (loadMode == LoadSceneMode.Additive)
            {
                LocationLoaded?.Invoke();
            }
        }
    }
}