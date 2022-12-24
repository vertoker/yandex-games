using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class DeathableObject : MonoBehaviour
    {
        [SerializeField] private bool isBullet = false;
        public Action DeathEvent;
        private bool isDead;

        private void OnEnable()
        {
            isDead = false;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if ((collision.gameObject.CompareTag("Bullet") || isBullet) && !isDead)
            {
                DeathEvent?.Invoke();
                isDead = true;
            }
        }
    }
}