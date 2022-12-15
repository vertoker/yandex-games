using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Location
{
    public class TriggerCaller : MonoBehaviour
    {
        public Action<Collider> triggerEnterEvent;
        public Action<Collider> triggerExitEvent;

        private void OnTriggerEnter(Collider other)
        {
            triggerEnterEvent?.Invoke(other);
        }
        private void OnTriggerExit(Collider other)
        {
            triggerExitEvent?.Invoke(other);
        }
    }
}