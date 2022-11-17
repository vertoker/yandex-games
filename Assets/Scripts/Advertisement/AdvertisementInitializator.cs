using System.Collections;
using UnityEngine.Events;
using Agava.YandexGames;
using UnityEngine;

namespace Scripts.Advertisement
{
    public class AdvertisementInitializator : MonoBehaviour
    {
        //[SerializeField] private bool debug = false;
        private static UnityEvent<bool> initializatorEvent = new UnityEvent<bool>();

        public static event UnityAction<bool> Initialize
        {
            add => initializatorEvent.AddListener(value);
            remove => initializatorEvent.RemoveListener(value);
        }

        private void Start()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
                InitializeCallback();
            else
                YandexGamesSdk.Initialize(InitializeCallback);
        }
        private void InitializeCallback()
        {
            initializatorEvent.Invoke(Application.platform == RuntimePlatform.WindowsEditor);
        }
    }
}