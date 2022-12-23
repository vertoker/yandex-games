using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class DeathableObject : MonoBehaviour
    {
        public Action deathEvent;
        private bool isDead;

        private void OnEnable()
        {
            isDead = false;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (isDead) return;
            deathEvent?.Invoke();
            isDead = true;
        }
    }
}