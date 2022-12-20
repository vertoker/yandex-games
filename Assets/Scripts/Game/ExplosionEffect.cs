using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ExplosionEffect : MonoBehaviour
    {
        [SerializeField] private DeathableObject self;
        private ParticleSystem effect;

        private void Awake()
        {
            effect = GetComponent<ParticleSystem>();
        }
        private void OnEnable()
        {
            self.deathEvent += Explode;
        }
        private void OnDisable()
        {
            self.deathEvent -= Explode;
        }
        private void Explode()
        {
            transform.position = self.transform.position;
            effect.Play();
        }
    }
}
