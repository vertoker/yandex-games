using System;
using UnityEngine;

namespace Game
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