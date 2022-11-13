using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;

        public string Name => _name;
        public Sprite Sprite => _sprite;

        public void Set(string name, Sprite sprite)
        {
            _name = name;
            _sprite = sprite;
        }
    }
}