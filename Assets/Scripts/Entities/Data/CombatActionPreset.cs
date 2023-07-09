using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Data
{
    [CreateAssetMenu(menuName = "Game/" + nameof(CombatActionPreset), fileName = nameof(CombatActionPreset))]
    public class CombatActionPreset : ScriptableObject
    {
        [SerializeField] private AnimationClip clip;
        [SerializeField] public InputDirection inputDirection;
        [SerializeField] public InputCombat inputCombat;
        [Space]
        [SerializeField] public CombatActionPreset[] nextActions;

        public AnimationClip Clip => clip;
        public InputDirection InputDirection => inputDirection;
        public InputCombat InputCombat => inputCombat;
        public CombatActionPreset[] NextActions => nextActions;
    }
}