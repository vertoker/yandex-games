using UnityEngine;

namespace Entities.Data
{
    public class EntityState : MonoBehaviour
    {
        [SerializeField] private EntityStatsPreset preset;
        
        [Space]
        [SerializeField] private int health;
    }
}
