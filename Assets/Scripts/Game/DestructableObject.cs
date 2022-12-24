using System;
using UnityEngine;

namespace Game
{
    public class DestructableObject : MonoBehaviour
    {
        [SerializeField] private Transform selfTransform;
        [SerializeField] private Rigidbody selfRigidbody;
        private Vector3Int objectSize;

        public Vector3 Position => selfTransform.position;
        public Vector3 Rotation => selfTransform.eulerAngles;
        public Vector3Int Scale => objectSize;
        public Transform Transform => selfTransform;
        public Rigidbody Rigidbody => selfRigidbody;
        public bool IsDestructable => objectSize != ExplosionComputator.EPSILONSIZE;

        private void Awake()
        {
            selfTransform = transform;
            selfRigidbody = GetComponent<Rigidbody>();
        }
        private void OnEnable()
        {
            objectSize = Vector3Int.CeilToInt(selfTransform.localScale);
            selfRigidbody.mass = objectSize.x * objectSize.y * objectSize.z;
        }
    }
}