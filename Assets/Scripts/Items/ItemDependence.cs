using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
    public class ItemDependence : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string[] _dependencies;

        public string Name => _name;
        public string[] Dependencies => _dependencies;

        public void Set(string name, string[] dependencies)
        {
            _name = name;
            _dependencies = dependencies;
        }
    }
}