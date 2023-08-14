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

        public void SetupColors(ColorData[] colors)
        {
            for (var i = 0; i < colors.Length; i++)
            {
                var data = _pool.Dequeue();
                data.Init(colors[i], i + 1);
            }
        }

        public void Dispose()
        {
            _pool.EnqueueAll();
        }
    }
}