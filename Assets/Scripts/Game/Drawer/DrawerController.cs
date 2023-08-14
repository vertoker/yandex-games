using Preset;
using TMPro;
using UnityEngine;

namespace Game.Drawer
{
    public class DrawerController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer background;
        [SerializeField] private SpriteRenderer result;
        [SerializeField] private int poolInit = 1000;
        
        [Header("Pixels")]
        [SerializeField] private SpriteRenderer pixelPreset;
        [SerializeField] private Transform pixelParent;
        private Pool<SpriteRenderer> _pixelPool;
        
        [Header("Text")]
        [SerializeField] private TextMeshPro textPreset;
        [SerializeField] private Transform textParent;
        private Pool<TextMeshPro> _textPool;

        private void Awake()
        {
            _pixelPool = new Pool<SpriteRenderer>(pixelPreset, pixelParent, poolInit);
            _textPool = new Pool<TextMeshPro>(textPreset, textParent, poolInit);
        }

        public void Init(ImagePreset preset)
        {
            var texture = preset.ImageSource.texture;
            
        }
        public void Dispose()
        {
            _pixelPool.EnqueueAll();
            _textPool.EnqueueAll();
        }
    }
}