using System;
using UnityEngine;
using YG;

namespace Game.Menu
{
    public class WatchAdDialogWindow : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        private static WatchAdDialogWindow _instance;

        public static event Action<int> SuccessEvent;
        public static event Action FailEvent;

        private void Awake()
        {
            _instance = this;
        }

        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += Success;
        }
        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= Success;
        }
        private static void Success(int id)
        {
            SuccessEvent?.Invoke(id);
        }

        public static void Open()
        {
            _instance.target.SetActive(true);
        }
        public void Yes()
        {
            YandexGame.RewVideoShow(1);
            target.SetActive(false);
        }
        public void No()
        {
            FailEvent?.Invoke();
            target.SetActive(false);
        }
    }
}