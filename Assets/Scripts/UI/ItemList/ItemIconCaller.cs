using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.ItemList {
    public class ItemIconCaller : MonoBehaviour
    {
        [SerializeField] private ItemList list;
        private int id;

        private void Awake()
        {
            var temp = gameObject.name.Split('(')[1];
            id = int.Parse(temp.Replace(")", string.Empty));
        }
        public void Click()
        {
            list.ClickItem(id);
        }
        public void BeginDrag()
        {
            list.BeginDragItem(id);
        }
    }
}