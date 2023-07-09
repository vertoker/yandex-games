using Camera;
using Camera.Watchers;
using UnityEngine;

namespace UI.Windows
{
    public class CombatWindow : BaseWindow
    {
        [SerializeField] private CombatWatcher combatWatcher;
        
        public override void Enable()
        {
            CameraController.Switch(combatWatcher);
        }
    }
}