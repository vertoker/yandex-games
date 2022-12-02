using System.Collections;
using UnityEngine;
using System;

namespace Scripts.Game
{
    public class Inializator : MonoBehaviour
    {
        public static event Action InitializationComplete;

        private void Start()
        {
            InitializationComplete.Invoke();
        }
    }
}