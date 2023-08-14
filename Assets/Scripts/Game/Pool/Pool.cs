using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Pool<TComponent> where TComponent : Component
    {
        private readonly TComponent _preset;
        private readonly Transform _parent;
        
        private readonly Queue<TComponent> _pool;
        public readonly List<TComponent> Actives;

        private int _currentCount;

        public Pool(TComponent preset, Transform parent, int initCount)
        {
            _preset = preset;
            _parent = parent;
            _pool = new Queue<TComponent>(initCount);
            Actives = new List<TComponent>(initCount);

            _currentCount = initCount;
            for (var i = 0; i < initCount; i++)
            {
                var item = Spawn();
                _pool.Enqueue(item);
            }
        }

        public void Enqueue(TComponent item)
        {
            Actives.Remove(item);
            _currentCount++;
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }
        public TComponent Dequeue()
        {
            TComponent item;
            if (_currentCount == 0)
            {
                item = Spawn();
            }
            else
            {
                item = _pool.Dequeue();
                _currentCount--;
            }
            
            Actives.Add(item);
            item.gameObject.SetActive(true);
            return item;
        }

        public void EnqueueAll()
        {
            var length = Actives.Count;
            for (var i = 0; i < length; i++)
            {
                var item = Actives[i];
                item.gameObject.SetActive(false);
                _pool.Enqueue(item);
            }
            
            _currentCount += length;
            Actives.Clear();
        }
        public List<TComponent> DequeueAll()
        {
            for (var i = 0; i < _currentCount; i++)
            {
                var item = _pool.Dequeue();
                item.gameObject.SetActive(true);
                Actives.Add(item);
            }
            _currentCount = 0;
            return Actives;
        }

        private TComponent Spawn()
        {
            var item = Object.Instantiate(_preset, _parent);
            item.gameObject.SetActive(false);
            return item;
        }
    }
}