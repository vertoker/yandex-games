using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Drawer
{
    public class ColorButtonsHandler : MonoBehaviour
    {
        [SerializeField] private int poolInit = 30;
        [SerializeField] private ColorButton preset;
        [SerializeField] private Transform parent;

        private Pool<ColorButton> _pool;

        private void Awake()
        {
            _pool = new Pool<ColorButton>(preset, parent, poolInit);
        }

        public void SetupColors(Dictionary<Color, int> colors)
        {
            var counter = 1;
            foreach (var pair in colors)
            {
                var data = _pool.Dequeue();
                data.Init(pair.Key, pair.Value, counter);
                counter++;
            }
        }

        public void Dispose()
        {
            _pool.EnqueueAll();
        }
    }
}