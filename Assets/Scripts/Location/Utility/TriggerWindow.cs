using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Game.UI;
using Scripts.Tools;

namespace Scripts.Location.Utility
{
    public class TriggerWindow : MonoBehaviour
    {
        [SerializeField] private WindowType type = WindowType.Static;
        [SerializeField] private string key = string.Empty;

        private void OnTriggerEnter(Collider other)
        {
            WindowHandler.Open(type, key);
        }
        private void OnTriggerExit(Collider other)
        {
            WindowHandler.Close(type, key);
        }
    }
}