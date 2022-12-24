using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Effect : MonoBehaviour
    {
        private Transform self;
        private ParticleSystem effect;

        private void Awake()
        {
            self = transform;
            effect = GetComponent<ParticleSystem>();
        }
        public void Explode(Vector3 position)
        {
            self.position = position;
            effect.Play();
        }
    }
}
