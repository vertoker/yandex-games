using UnityEngine;

namespace Utility.Loading
{
    public abstract class LoadIconFormula : ScriptableObject
    {
        [SerializeField] private int pointsCount;
        [SerializeField] private float timeScale;

        public int PointsCount => pointsCount;
        public float TimeScale => timeScale;

        public abstract Vector2 Formula(float progress);
    }
}