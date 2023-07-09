using UnityEngine;

namespace Camera.Watchers
{
    public class CombatWatcher : WatcherBase
    {
        [SerializeField] private CombatWatcherPreset preset;
        
        public override void BeginSwitch()
        {
            Target.position = preset.Pos;
            Target.rotation = preset.Rot;
        }
        public override void UpdateWatch()
        {
            
        }
        public override void EndSwitch()
        {
            
        }
    }
}