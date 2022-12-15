using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Location.Extraction;

namespace Scripts.Game.Controller
{
    [RequireComponent(typeof(Animator))]
    public class PersonAnimatorController : MonoBehaviour
    {
        [SerializeField] private float deadZone = 0.05f;
        private AnimationKey activeAnim = AnimationKey.Idle;
        private bool inAction = false;
        private Animator animator;

        private Dictionary<AnimationKey, string> animations = new Dictionary<AnimationKey, string>()
        {
            { AnimationKey.Idle, "Idle" },
            { AnimationKey.Walking, "Walk" },
            { AnimationKey.Extraction, "Extract" },
            { AnimationKey.Talking, "Talk" },
            { AnimationKey.Crafting, "Craft" }
        };

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Move(Vector2 direction, float speed)
        {
            if (inAction)
                return;
            if (direction.magnitude > deadZone)
                SwitchAnim(AnimationKey.Walking, direction.magnitude * speed);
            else
                SwitchAnim(AnimationKey.Idle);
        }
        public void StartAction(AnimationKey key)
        {
            if (inAction)
                EndAction();
            inAction = true;
            SwitchAnim(key);
        }
        public void EndAction(bool doIdle = false)
        {
            inAction = false;
            if (doIdle)
                SwitchAnim(AnimationKey.Idle);
        }
        private void SwitchAnim(AnimationKey key, float speed = 1f)
        {
            activeAnim = key;
            animator.speed = speed;
            animator.Play(animations[key]);
        }
    }

    public enum AnimationKey : byte
    {
        Idle = 0,
        Walking = 1,
        Extraction = 2,
        Talking = 3,
        Crafting = 4
    }
}