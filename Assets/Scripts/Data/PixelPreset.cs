using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Presets/" + nameof(PixelPreset), fileName = nameof(PixelPreset))]
    public class PixelPreset : ScriptableObject
    {
        [SerializeField] private float gridOffset = 0.1f;

        public Vector2 GetPos(int x, int y, int width, int height)
        {
            var posX = (x - width / 2f) * gridOffset + gridOffset / 2f;
            var posY = (y - height / 2f) * gridOffset + gridOffset / 2f;
            return new Vector2(posX, posY);
        }

        public Vector2 GetSize(int width, int height)
        {
            var sizeX = width / 2f * gridOffset;
            var sizeY = height / 2f * gridOffset;
            return new Vector2(sizeX, sizeY);
        }
    }
}