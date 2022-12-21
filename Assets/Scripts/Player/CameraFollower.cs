using System;
using Game;
using UnityEngine;
using Utility;

namespace Player
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float lerpPositionTime = 0.3f;
        [SerializeField] private float lerpAngleTime = 0.3f;
        [SerializeField] private Vector3 lookAtOffset = new Vector3(0f, 1f, 0);
        [SerializeField] private Vector3 positionOffset = new Vector3(-7f, 2.5f, 0);
        [SerializeField] private Transform target;
        
        private Transform self;
        private bool isCinematic = false;
        private Vector3 cinematicTargetPos;
        private Vector3 cinematicTargetRot;

        private void Awake()
        {
            self = transform;
        }
        private void OnEnable()
        {
            GameCycle.StartCycleEvent += DisableCinematic;
        }
        private void OnDisable()
        {
            GameCycle.StartCycleEvent -= DisableCinematic;
        }

        private void FixedUpdate()
        {
            if (isCinematic)
            {
                self.position = Vector3.Lerp(self.position, cinematicTargetPos, lerpPositionTime);
                self.localEulerAngles = CustomMath.AngleLerp(self.localEulerAngles, cinematicTargetRot, lerpAngleTime);
            }
            else
            {
                var angles = target.localEulerAngles;
                var position = target.position;

                var targetPosition = position + CustomMath.RotateVector(positionOffset, angles);
                self.position = Vector3.Lerp(self.position, targetPosition, lerpPositionTime);
                self.localEulerAngles = CustomMath.AngleLerp(self.localEulerAngles, angles, lerpAngleTime);
            }
        }

        public void EnableCinematic(Vector3 pos, Vector3 rot, bool instanceMove = false)
        {
            cinematicTargetPos = pos;
            cinematicTargetRot = rot;
            isCinematic = true;
            if (!instanceMove) return;
            self.position = pos;
            self.localEulerAngles = rot;
        }
        public void DisableCinematic()
        {
            isCinematic = false;
        }
    }
}