using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Location.Utility
{
    public class TriggerObjectActive : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private bool unactiveState = false;

        private void Awake()
        {
            target.SetActive(unactiveState);
        }
        private void OnTriggerEnter(Collider other)
        {
            target.SetActive(!unactiveState);
        }
        private void OnTriggerExit(Collider other)
        {
            target.SetActive(unactiveState);
        }
    }
}