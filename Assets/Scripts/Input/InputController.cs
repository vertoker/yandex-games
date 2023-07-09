using System;
using Entities.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Input
{
    public class InputController : MonoBehaviour
    {
        public InputDirection inputDirection = InputDirection.None;
        public InputCombat inputCombat = InputCombat.None;
        
        private Action<InputDirection, InputCombat> _inputAction;

        public event Action<InputDirection, InputCombat> InputAction
        {
            add => _inputAction += value;
            remove => _inputAction -= value;
        }
        
        public void StartAction()
        {
            Debug.Log($"StartAction Direction:{inputDirection} Combat:{inputCombat}");
            _inputAction?.Invoke(inputDirection, inputCombat);
        }
    }
}