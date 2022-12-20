using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PoolObjects : MonoBehaviour
    {
        [SerializeField] private int initSpawnCount = 100;
        [SerializeField] private GameObject origin;
        private List<DestructableObject> list;
        private Queue<DestructableObject> pool;
        private Transform self;

        private void Awake()
        {
            self = transform;
            list = new List<DestructableObject>();
            pool = new Queue<DestructableObject>();

            for (int i = 0; i < self.childCount; i++)
            {
                list.Add(self.GetChild(i).GetComponent<DestructableObject>());
            }
            for (int i = 0; i < initSpawnCount; i++)
            {
                pool.Enqueue(Instantiate(origin, self).GetComponent<DestructableObject>());
            }
        }

        public DestructableObject Dequeue()
        {
            if (pool.Count == 0)
                return CreateNew();
            return pool.Dequeue();
        }
        public void Enqueue(DestructableObject obj)
        {
            pool.Enqueue(obj);
        }
        private DestructableObject CreateNew()
        {
            return Instantiate(origin, self).GetComponent<DestructableObject>();
        }
    }
}