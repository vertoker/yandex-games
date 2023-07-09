using UnityEngine;

namespace Utility.Controller
{
    public class ControllerInheritor<T> : MonoBehaviour
    {
        protected T Controller;
        
        public void Init(T controller)
        {
            Controller = controller;
        }
        
        public virtual void Enable()
        {
            
        }
        public virtual void Disable()
        {
            
        }
    }
}