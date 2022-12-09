using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Tools;

namespace Scripts.Game
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float timeLerp = 0.15f;
        [SerializeField] private float heigth;
        [SerializeField] private float minDistance = 4;
        [SerializeField] private float maxDistance = 8;
        [SerializeField] private Transform target;
        private Transform self;

        private void Awake()
        {
            self = GetComponent<Transform>();
        }
        private void FixedUpdate()
        {
            var delta = target.position - self.position;
            var currentDistance = Mathf.Sqrt(delta.x * delta.x + delta.z * delta.z);
            if (currentDistance < minDistance)
            {
                delta = delta / currentDistance * minDistance;
                self.position = Vector3.Lerp(self.position, target.position - delta, timeLerp);
            }
            else if (currentDistance > maxDistance)
            {
                delta = delta / currentDistance * maxDistance;
                self.position = Vector3.Lerp(self.position, target.position - delta, timeLerp);
            }
            self.position = new Vector3(self.position.x, heigth, self.position.z);
            self.LookAt(target.position);
        }
    }
}