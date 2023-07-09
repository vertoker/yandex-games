using System.Collections;
using UnityEngine;
using Utility.Controller;

namespace Input.Providers
{
    public class BaseProvider : ControllerInheritor<InputController>
    {
        protected bool Active;
        private Coroutine _inputUpdate;

        public override void Enable()
        {
            Active = true;
            _inputUpdate = StartCoroutine(InputUpdateCoroutine());
        }
        public override void Disable()
        {
            Active = false;
        }

        private IEnumerator InputUpdateCoroutine()
        {
            while (Active)
            {
                yield return null;
                InputUpdate();
            }
        }
        public virtual void InputUpdate()
        {
            
        }
    }
}