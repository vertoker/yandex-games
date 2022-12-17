using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float lerpTime = 0.3f;
        [SerializeField] private Vector3 offset = new Vector3(-7f, 2.5f, 0);
        [SerializeField] private Transform target;
        private Transform self;

        private void Awake()
        {
            self = transform;
        }

        private void FixedUpdate()
        {
            self.position = Vector3.Lerp(self.position, target.position + offset, Time.deltaTime);
        }
    }
}