using Game;
using UnityEngine;

namespace Player
{
    public class TrailActivable : MonoBehaviour
    {
        [SerializeField] private GameObject obj;

        private void OnEnable()
        {
            GameCycle.StartCycleEvent += EnableTrail;
            GameCycle.EndFuelEvent += DisableTrail;
        }
        private void OnDisable()
        {
            GameCycle.StartCycleEvent -= EnableTrail;
            GameCycle.EndFuelEvent -= DisableTrail;
        }

        private void EnableTrail()
        {
            obj.SetActive(true);
        }
        private void DisableTrail()
        {            
            obj.SetActive(false);
        }
    }
}
