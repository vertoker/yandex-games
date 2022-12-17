using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts.Game
{
    public class DeathableObject : MonoBehaviour
    {
        public Action deathEvent;

        private void OnCollisionEnter(Collision collision)
        {
            deathEvent?.Invoke();
        }
    }
}