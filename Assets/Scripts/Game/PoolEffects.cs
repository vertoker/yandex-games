using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable MemberCanBePrivate.Global

namespace Game
{
    public class PoolEffects : MonoBehaviour
    {
        [SerializeField] private Effect origin;
        [SerializeField] private int initEffects = 10;
        [SerializeField] private float timeEffectExistance = 1f;
        private Queue<Effect> pool = new Queue<Effect>();

        private Transform parent;
        private static PoolEffects _self;
        private void Awake()
        {
            _self = this;
            parent = transform;
            for (int i = 0; i < initEffects; i++)
            {
                pool.Enqueue(Instantiate(origin, parent));
            }
        }
        
        public static void ExecuteExplosion(Explosion explosion)
        {
            var effect = Dequeue();
            effect.Explode(explosion.Position);
            _self.StartCoroutine(DisableEffect(effect));
        }
        public static void Enqueue(Effect effect)
        {
            _self.pool.Enqueue(effect);
        }
        public static Effect Dequeue()
        {
            if (_self.pool.Count == 0)
                return _self.CreateNew();
            return _self.pool.Dequeue();
        }
        private Effect CreateNew()
        {
            return Instantiate(origin, parent);
        }
        
        private static IEnumerator DisableEffect(Effect effect)
        {
            yield return new WaitForSeconds(_self.timeEffectExistance);
            Enqueue(effect);
        }
    }
}
