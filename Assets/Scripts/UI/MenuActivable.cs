using System;
using Game;
using UnityEngine;

namespace UI
{
    public class MenuActivable : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        
        private void OnEnable()
        {
            GameCycle.StartMenuEvent += SetActive;
            GameCycle.EndMenuEvent += SetUnactive;
        }

        private void OnDisable()
        {
            GameCycle.StartMenuEvent -= SetActive;
            GameCycle.EndMenuEvent -= SetUnactive;
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
