using System.Collections;
using UnityEngine;
using System;

namespace Game
{
    public class GameCycle : MonoBehaviour
    {
        public float time = 10;
        public int bullets = 3;
        
        public static Action StartGameEvent, EndGameEvent;
        public static Action StartCycleEvent, EndCycleEvent;
        public static Action<float> UpdateFuelEvent;

        private void Start()
        {
            StartGame();
        }
        public void StartGame()
        {
            StartGameEvent?.Invoke();
            StartCoroutine(FuelTimer());
        }
        public void EndGame()
        {
            EndCycleEvent?.Invoke();
            EndGameEvent?.Invoke();
        }
        private IEnumerator FuelTimer()
        {
            StartCycleEvent?.Invoke();
            for (float i = 1; i >= 0; i -= Time.deltaTime / time)
            {
                UpdateFuelEvent?.Invoke(i);
                yield return null;
            }
            EndCycleEvent?.Invoke();
        }

        public static void Death()
        {
            EndCycleEvent?.Invoke();
        }
    }
}
