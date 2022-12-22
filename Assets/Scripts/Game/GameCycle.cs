using System.Collections;
using UnityEngine;
using System;
using YG;

namespace Game
{
    public class GameCycle : MonoBehaviour
    {
        [SerializeField] private float lookSideTime = 1;
        
        private int activeBulletCapacity;
        
        public static Action StartMenuEvent, EndMenuEvent;
        public static Action StartGameEvent, EndGameEvent;
        public static Action StartCycleEvent, EndFuelEvent, EndCycleEvent;
        public static Action<float> UpdateFuelEvent;
        private static GameCycle _instance;

        private Coroutine fuelUpdater;

        private void Awake()
        {
            _instance = this;
        }
        private void OnEnable()
        {
            YandexGame.GetDataEvent += LoadData;
        }
        private void OnDisable()
        {
            YandexGame.GetDataEvent -= LoadData;
        }

        private void LoadData()
        {
            StartMenuEvent?.Invoke();
        }
        public void StartGame()
        {
            EndMenuEvent?.Invoke();
            activeBulletCapacity = YandexGame.savesData.bulletCount;
            StartGameEvent?.Invoke();
            fuelUpdater = StartCoroutine(FuelTimer());
        }
        public void EndGame()
        {
            if (fuelUpdater != null)
                StopCoroutine(fuelUpdater);
            EndCycleEvent?.Invoke();
            EndGameEvent?.Invoke();
            StartMenuEvent?.Invoke();
        }
        private IEnumerator FuelTimer()
        {
            StartCycleEvent?.Invoke();
            for (float i = 1; i >= 0; i -= Time.deltaTime / YandexGame.savesData.fuelTime)
            {
                UpdateFuelEvent?.Invoke(i);
                yield return null;
            }
            EndFuelEvent?.Invoke();
        }

        private IEnumerator LookSideTimer()
        {
            yield return new WaitForSeconds(lookSideTime);
            if (activeBulletCapacity > 1)
            {
                activeBulletCapacity--;
                fuelUpdater = StartCoroutine(FuelTimer());
            }
            else
            {
                EndGame();
            }
        }

        public static void Death()
        {
            print("death");
            EndCycleEvent?.Invoke();
            if (_instance.fuelUpdater != null)
                _instance.StopCoroutine(_instance.fuelUpdater);
            _instance.StartCoroutine(_instance.LookSideTimer());
        }

        public void SetTimeScale(float time)
        {
            Time.timeScale = time;
        }
    }
}
