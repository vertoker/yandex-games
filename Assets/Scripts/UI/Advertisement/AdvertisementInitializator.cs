using UnityEngine.EventSystems;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Advertisement
{
    public class AdvertisementInitializator : MonoBehaviour
    {
        private static UnityEvent initializatorEvent = new UnityEvent();

        public static event UnityAction Initialize
        {
            add => initializatorEvent.AddListener(value);
            remove => initializatorEvent.RemoveListener(value);
        }

        private void Start()
        {
            YandexGamesSdk.Initialize(InitializeCallback);
        }

        private void InitializeCallback()
        {
            initializatorEvent.Invoke();
        }
    }
}