using UnityEngine;

namespace Core.Camera.Watchers
{
    [CreateAssetMenu(menuName = "Game/" + nameof(CombatWatcherPreset), fileName = nameof(CombatWatcherPreset))]
    public class CombatWatcherPreset : ScriptableObject
    {
        [SerializeField] private Vector3 pos;
        [SerializeField] private Quaternion rot;
    }
}