using System;
using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Int", fileName = "IntUpgrade")]
    public class IntParam : ScriptableObject
    {
        [SerializeField] private IntUpgrade[] upgrades;

        public int[] Prices => upgrades.Select(a => a.price).ToArray();
        public int[] Values => upgrades.Select(a => a.value).ToArray();
        public int Length => upgrades.Length;
        
        /*public float GetNextValue(int current, out bool billing)
        {
            billing = false;
            int length = values.Length;
            for (int i = 0; i < length; i++)
            {
                if (values[i] == current && i < length - 1)
                {
                    current = values[i + 1];
                    billing = true;
                    break;
                }
            }
            return current;
        }*/
    }
    
    [Serializable]
    public class IntUpgrade
    {
        public int price;
        public int value;

        public IntUpgrade(int price, int value)
        {
            this.price = price;
            this.value = value;
        }
    }
}
