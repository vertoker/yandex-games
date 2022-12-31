using System;
using UnityEngine;
using System.Linq;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Float", fileName = "FloatUpgrade")]
    public class FloatParam : ScriptableObject
    {
        [SerializeField] private FloatUpgrade[] upgrades;

        public int[] Prices => upgrades.Select(a => a.price).ToArray();
        public float[] Values => upgrades.Select(a => a.value).ToArray();
        public int Length => upgrades.Length;
    }
    
    [Serializable]
    public class FloatUpgrade
    {
        public int price;
        public float value;

        public FloatUpgrade(int price, float value)
        {
            this.price = price;
            this.value = value;
        }
    }
}
