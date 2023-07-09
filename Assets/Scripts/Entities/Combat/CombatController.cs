using System;
using Entities.Data;
using Input;
using UnityEngine;

namespace Entities.Combat
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private CombatActionPreset rootCombat;
        [SerializeField] private Animator animator;

        [Header("Debug")]
        [SerializeField] private CombatActionPreset currentAction;

        private void OnEnable()
        {
            inputController.InputAction += InputAction;
        }
        private void OnDisable()
        {
            inputController.InputAction -= InputAction;
        }
        private void Start()
        {
            StartAction(rootCombat);
        }

        private void InputAction(InputDirection direction, InputCombat combat)
        {
            
        }

        private void StartAction(CombatActionPreset action)
        {
            currentAction = action;
            
            animator.Play(currentAction.Clip.name);
        }
    }
}