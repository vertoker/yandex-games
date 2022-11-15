using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.UI;

namespace Scripts.UI.ItemList
{
    public class ItemList : MonoBehaviour
    {
        private AnimatorUI listWindowAnimator;

        private void OnEnable()
        {
            listWindowAnimator.OpenStartEvent += UpdateList;
        }
        private void OnDisable()
        {
            listWindowAnimator.OpenStartEvent -= UpdateList;
        }

        private void UpdateList()
        {

        }
    }
}