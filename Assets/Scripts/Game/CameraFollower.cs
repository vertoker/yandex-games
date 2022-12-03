using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float timeLerp = 0.15f;
        [SerializeField] private float heigth;
        [SerializeField] private Vector2 offsetPosition;
        [SerializeField] private Vector3 offsetRotation;
        [SerializeField] private Transform target;
        private Transform self;

        private void Awake()
        {
            self = GetComponent<Transform>();
        }
        private void FixedUpdate()
        {
            var targetPos = new Vector3(target.position.x + offsetPosition.x, heigth, target.position.z + offsetPosition.y);
            self.position = Vector3.Lerp(self.position, targetPos, timeLerp);
            self.LookAt(target.position);
            //self.eulerAngles = offsetRotation;
        }
    }
}