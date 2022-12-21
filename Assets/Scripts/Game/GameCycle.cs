using System.Collections;
using UnityEngine;
using System;

namespace Game
{
    public class GameCycle : MonoBehaviour
    {
        public float lookSideTime = 1f;
        public float time = 10;
        public int bullets = 3;

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
        private void Start()
        {
            StartMenuEvent?.Invoke();
        }
        public void StartGame()
        {
            EndMenuEvent?.Invoke();
            StartGameEvent?.Invoke();
            activeBulletCapacity = bullets;
            fuelUpdater = StartCoroutine(FuelTimer());
        }
        public void EndGame()
        {
            EndCycleEvent?.Invoke();
            EndGameEvent?.Invoke();
            StartMenuEvent?.Invoke();
        }
        private IEnumerator FuelTimer()
        {
            StartCycleEvent?.Invoke();
            for (float i = 1; i >= 0; i -= Time.deltaTime / time)
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
                EndGameEvent?.Invoke();
                StartMenuEvent?.Invoke();
            }
        }

        public static void Death()
        {
            EndCycleEvent?.Invoke();
            if (_instance.fuelUpdater != null)
                _instance.StopCoroutine(_instance.fuelUpdater);
            _instance.StartCoroutine(_instance.LookSideTimer());
        }
    }
}
