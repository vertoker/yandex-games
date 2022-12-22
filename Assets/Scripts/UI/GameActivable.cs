using System;
using Game;
using UnityEngine;

namespace UI
{
    public class GameActivable : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        
        private void OnEnable()
        {
            GameCycle.StartGameEvent += SetActive;
            GameCycle.EndGameEvent += SetUnactive;
        }

        private void OnDisable()
        {
            GameCycle.StartGameEvent -= SetActive;
            GameCycle.EndGameEvent -= SetUnactive;
        }

        private void SetActive()
        {
            target.SetActive(true);
        }
        private void SetUnactive()
        {
            target.SetActive(false);
        }
    }
}
