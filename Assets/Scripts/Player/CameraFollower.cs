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
        [Space] [SerializeField] private float minDistance = 1f;
        [SerializeField] private float maxDistance = 15f;
        
        private Transform self;
        private Transform cam;
        private CameraType type;
        
        private Vector3 cinematicTargetPos;
        private Vector3 cinematicTargetRot;

        private void Awake()
        {
            self = transform;
            cam = self.GetChild(0);
        }
        private void OnEnable()
        {
            GameCycle.EndFuelEvent += EnableFollowerAside;
            GameCycle.StartCycleEvent += DisableCinematic;
        }
        private void OnDisable()
        {
            GameCycle.EndFuelEvent -= EnableFollowerAside;
            GameCycle.StartCycleEvent -= DisableCinematic;
        }

        private void FixedUpdate()
        {
            if (type == CameraType.FollowerController)
            {
                var angles = target.localEulerAngles;
                var position = target.position;

                var targetPosition = position + CustomMath.RotateVector(positionOffset, angles);
                self.position = Vector3.Lerp(self.position, targetPosition, lerpPositionTime);
                self.localEulerAngles = CustomMath.AngleLerp(self.localEulerAngles, angles, lerpAngleTime);
            }
            else if (type == CameraType.Cinematic)
            {
                self.position = Vector3.Lerp(self.position, cinematicTargetPos, lerpPositionTime);
                self.localEulerAngles = CustomMath.AngleLerp(self.localEulerAngles, cinematicTargetRot, lerpAngleTime);
            }
            else
            {
                var position = target.position;
                var delta = self.position - position;
                self.position = position + delta.normalized * Mathf.Clamp(delta.magnitude, minDistance, maxDistance);
                self.LookAt(position);
            }
        }
        
        public void EnableCinematic(Vector3 pos, Vector3 rot, bool instantMove = false, bool rotateCam = true)
        {
            cinematicTargetPos = pos;
            cinematicTargetRot = rot;
            type = CameraType.Cinematic;
            cam.localEulerAngles = new Vector3(0,rotateCam ? 90 : 0, 0);
            if (!instantMove) return;
            self.position = pos;
            self.localEulerAngles = rot;
        }
        public void DisableCinematic()
        {
            type = CameraType.FollowerController;
            var angle = target.localEulerAngles;
            cam.localEulerAngles = new Vector3(0,90, 0);
            self.localEulerAngles = new Vector3(0f, angle.y, angle.z);
        }

        public void EnableFollowerAside()
        {
            type = CameraType.FollowerAside;
            cam.localEulerAngles = Vector3.zero;
        }
        public void DisableFollowerAside()
        {
            type = CameraType.FollowerController;
            cam.localEulerAngles = new Vector3(0,90, 0);
        }
    }

    public enum CameraType : byte
    {
        FollowerController = 0,
        FollowerAside = 1,
        Cinematic = 2
    }
}