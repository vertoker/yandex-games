using System;
using Game;
using UnityEngine;

namespace Player
{
    public class CameraLookSide : MonoBehaviour
    {
        [SerializeField] private float distance;
        [SerializeField] private Transform bullet;
        private CameraFollower follower;
        private Transform cam;

        private void Awake()
        {
            cam = transform;
            follower = GetComponent<CameraFollower>();
        }
        private void OnEnable()
        {
            GameCycle.EndCycleEvent += LookSideEnable;
            GameCycle.StartCycleEvent += LookSideDisable;
        }
        private void OnDisable()
        {
            GameCycle.EndCycleEvent -= LookSideEnable;
            GameCycle.StartCycleEvent -= LookSideDisable;
        }

        private void LookSideEnable()
        {
            var angle = cam.localEulerAngles;
            angle = new Vector3(0f, angle.y, angle.z);
            var pos = bullet.position;
            var direction = cam.position - pos;
            var posTarget = pos + direction.normalized * distance;
            follower.EnableCinematic(posTarget, angle);
        }
        private void LookSideDisable()
        {
            follower.DisableCinematic();
        }
    }
}
