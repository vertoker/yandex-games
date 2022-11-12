using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
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
    }
}