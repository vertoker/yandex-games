using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Data/Recipe")]
    public class Recipe : ScriptableObject
    {
        [SerializeField] private Item _output;
        [SerializeField] private Item[] _input;

        public Item Output => _output;
        public Item[] Input => _input;

        public void Set(Item output, Item[] input)
        {
            _output = output;
            _input = input;
        }

        public bool GetRecipe(string name1, string name2, string name3)
        {
            if (name3 != string.Empty)
            {
                if (name1 == _input[0].Name && name2 == _input[1].Name && name3 == _input[2].Name ||
                    name1 == _input[0].Name && name3 == _input[1].Name && name2 == _input[2].Name ||
                    name2 == _input[0].Name && name1 == _input[1].Name && name3 == _input[2].Name ||
                    name2 == _input[0].Name && name3 == _input[1].Name && name1 == _input[2].Name ||
                    name3 == _input[0].Name && name1 == _input[1].Name && name2 == _input[2].Name ||
                    name3 == _input[0].Name && name2 == _input[1].Name && name1 == _input[2].Name)
                {
                    return true;
                }
            }
            if (name1 == _input[0].Name && name2 == _input[1].Name || name2 == _input[0].Name && name1 == _input[1].Name)
            {
                return true;
            }
            return false;
        }
    }
}