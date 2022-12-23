using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PoolObjects : MonoBehaviour
    {
        [SerializeField] private int initSpawnCount = 100;
        [SerializeField] private DestructableObject origin;
        private Queue<DestructableObject> poolDisable;
        [SerializeField] private List<DestructableObject> listEnable;
        private Transform parent;

        private static PoolObjects _self;
        private void Awake()
        {
            _self = this;
            parent = transform;
            poolDisable = new Queue<DestructableObject>();
            listEnable = new List<DestructableObject>();
            for (int i = 0; i < initSpawnCount; i++)
            {
                poolDisable.Enqueue(Instantiate(origin, parent).GetComponent<DestructableObject>());
            }
        }

        public static DestructableObject Dequeue()
        {
            var obj = _self.poolDisable.Count == 0 ? _self.CreateNew() : _self.poolDisable.Dequeue();
            _self.listEnable.Add(obj);
            return obj;
        }
        public static void Enqueue(DestructableObject obj)
        {
            _self.listEnable.Remove(obj);
            _self.poolDisable.Enqueue(obj);
        }
        public static void EnqueueAll()
        {
            foreach (var obj in _self.listEnable)
            {
                obj.gameObject.SetActive(false);
                _self.poolDisable.Enqueue(obj);
            }
            _self.listEnable.Clear();
        }
        private DestructableObject CreateNew()
        {
            return Instantiate(origin, parent);
        }
    }
}