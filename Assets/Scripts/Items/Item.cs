using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
    public class Item : ScriptableObject
    {
        public string Name { get; set; }
        public Sprite Sprite { get; set; }
    }
}