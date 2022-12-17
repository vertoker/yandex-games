using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts.Game
{
    [RequireComponent(typeof(DeathableObject))]
    public class DestructableObject : MonoBehaviour
    {
        private GameObject selfObject;
        private Transform selfTransform;

        private void Awake()
        {
            selfObject = gameObject;
            selfTransform = transform;
        }
        public void Destruct(Vector3 epicenter, float radius, float power)
        {

        }
    }
}