using System;
using Input.Providers;
using UnityEngine;

namespace Input
{
    public class StartInputController : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private WasdProvider wasdProvider;

        private BaseProvider _active;

        public void OnEnable()
        {
            if (_active != null)
                _active.Disable();

            _active = GetActive();
            _active.Init(inputController);
            _active.Enable();
        }

        private BaseProvider GetActive()
        {
            return wasdProvider;
        }
    }
}