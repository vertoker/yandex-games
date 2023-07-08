using UnityEngine;

namespace Utility.Loading
{
    [CreateAssetMenu(menuName = "Data/Loading/" + nameof(InfinityFormula), fileName = nameof(InfinityFormula))]
    public class InfinityFormula : LoadIconFormula
    {
        [SerializeField] private Vector2 scale;
        
        public override Vector2 Formula(float progress)
        {
            if (progress < 0.5f)
            {
                var radians = Mathf.Repeat(progress * 4 + 1, 2) * Mathf.PI;
                return new Vector2(Mathf.Cos(radians) * scale.x + scale.x, Mathf.Sin(radians) * scale.y);
            }
            else
            {
                var radians = (4 - progress * 4) * Mathf.PI;
                return new Vector2(Mathf.Cos(radians) * scale.x - scale.x, Mathf.Sin(radians) * scale.y);
            }
        }
    }
}