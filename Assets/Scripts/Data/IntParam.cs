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
        
        public int GetNextValue(int current)
        {
            int length = Length - 1;
            for (int i = 0; i < length; i++)
                if (upgrades[i].value == current)
                    return upgrades[i + 1].value;
            return current;
        }
        public int GetPrice(int current)
        {
            int length = Length;
            for (int i = 0; i < length; i++)
                if (upgrades[i].value == current)
                    return upgrades[i].price;
            return 0;
        }
        public int GetIndex(int current)
        {
            int length = Length;
            for (int i = 0; i < length; i++)
                if (upgrades[i].value == current)
                    return i;
            return -1;
        }
        public int GetSpendMoney(int current)
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
