using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/Recipe")]
    public class Recipe : ScriptableObject
    {
        public Item Output { get; set; }
        public Item[] Input { get; set; }
    }
}