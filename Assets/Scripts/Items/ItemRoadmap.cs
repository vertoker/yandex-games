using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/ItemRoadmap")]
    public class ItemRoadmap : ScriptableObject
    {
        [SerializeField] private List<string> _list;

        public List<string> List => _list;

        public void Set(List<string> list)
        {
            _list = list;
        }
    }
}