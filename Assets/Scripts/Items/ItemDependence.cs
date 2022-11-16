using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
    public class ItemDependence : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string[] _dependencies2;
        [SerializeField] private string[] _dependencies3;

        public string Name => _name;
        public string[] Dependencies2 => _dependencies2;
        public string[] Dependencies3 => _dependencies3;

        public void Set(string name, string[] dependencies2, string[] dependencies3)
        {
            _name = name;
            _dependencies2 = dependencies2;
            _dependencies3 = dependencies3;
        }
    }
}