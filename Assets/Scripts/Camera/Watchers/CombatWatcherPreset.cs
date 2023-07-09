using UnityEngine;

namespace Camera.Watchers
{
    [CreateAssetMenu(menuName = "Game/" + nameof(CombatWatcherPreset), fileName = nameof(CombatWatcherPreset))]
    public class CombatWatcherPreset : ScriptableObject
    {
        [SerializeField] private Vector3 pos;
        [SerializeField] private Quaternion rot;

        public Vector3 Pos => pos;
        public Quaternion Rot => rot;
    }
}