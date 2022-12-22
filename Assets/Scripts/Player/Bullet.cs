using System;
using Game;
using UnityEngine;
using Utility;

namespace Player
{
    [RequireComponent(typeof(DeathableObject), typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Vector3 force = new Vector3(20f, 0f, 0f);
        private Vector3 activeAngle = Vector3.zero;
        private Vector3 currentAngle = Vector3.zero;
        private DeathableObject deathableObject;
        private Rigidbody rb;
        private Transform tr;

        private bool activeForce = false;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            tr = GetComponent<Transform>();
            deathableObject = GetComponent<DeathableObject>();
            currentAngle = tr.localEulerAngles;
        }
        private void OnEnable()
        {
            deathableObject.deathEvent += GameCycle.Death;
        }
        private void OnDisable()
        {
            deathableObject.deathEvent -= GameCycle.Death;
        }

        public void SetActiveForce(bool value)
        {
            activeForce = value;
            rb.useGravity = !value;
            if (!value) return;
            currentAngle = Vector3.zero;
            activeAngle = Vector3.zero;
        }
        
        private void FixedUpdate()
        {
            if (!activeForce)
                return;
            currentAngle += activeAngle;
            tr.localEulerAngles = currentAngle;
            rb.velocity = CustomMath.RotateVector(force, currentAngle);
        }
        public void MoveAngle(float x, float y)
        {
            activeAngle = new Vector3(0f, x, y);
        }
    }
}