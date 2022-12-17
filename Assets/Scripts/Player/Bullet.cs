using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Vector3 force = new Vector3(20f, 0f, 0f);
        [SerializeField] private Vector2 angle = new Vector2(3f, 3f);
        private Rigidbody self;

        private void Awake()
        {
            self = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            self.AddForce(force, ForceMode.Impulse);
        }
    }
}