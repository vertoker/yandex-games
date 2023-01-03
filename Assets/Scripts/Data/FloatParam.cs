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
        
        public float GetNextValue(float current)
        {
            int length = Length - 1;
            for (int i = 0; i < length; i++)
                if (upgrades[i].value == current)
                    return upgrades[i + 1].value;
            return current;
        }
        public int GetPrice(float current)
        {
            int length = Length;
            for (int i = 0; i < length; i++)
                if (upgrades[i].value == current)
                    return upgrades[i].price;
            return 0;
        }
        public int GetIndex(float current)
        {
            int length = Length;
            for (int i = 0; i < length; i++)
                if (upgrades[i].value == current)
                    return i;
            return -1;
        }
        public int GetSpendMoney(float current)
        {
            int money = 0, length = Length;
            for (int i = 0; i < length; i++)
            {
                if (upgrades[i].value == current)
                    break;
                money += upgrades[i].price;
            }
            return money;
        }
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
