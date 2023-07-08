using UnityEngine;

namespace Entities.Data
{
    [CreateAssetMenu(menuName = "Game/" + nameof(CombatActionPreset), fileName = nameof(CombatActionPreset))]
    public class CombatActionPreset : ScriptableObject
    {
        [SerializeField] private AnimationClip clip;
        [SerializeField] private WeaponType weapon;
        [SerializeField] public CombatActionInput inputAction;
        [Space]
        [SerializeField] public CombatActionPreset[] nextActions;

        public AnimationClip Clip => clip;
        public WeaponType Weapon => weapon;
        public CombatActionInput InputAction => inputAction;
        public CombatActionPreset[] NextActions => nextActions;
    }
}