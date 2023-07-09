using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Data;
using UnityEngine;

namespace Input.Data
{
    [CreateAssetMenu(menuName = "Game/" + nameof(WasdProviderPreset), fileName = nameof(WasdProviderPreset))]
    public class WasdProviderPreset : ScriptableObject
    {
        [SerializeField] private KeyCode[] left;
        [SerializeField] private KeyCode[] right;
        [SerializeField] private KeyCode[] up;
        [SerializeField] private KeyCode[] down;
        
        [SerializeField] private KeyCode[] hand;
        [SerializeField] private KeyCode[] leg;
        [SerializeField] private KeyCode[] block;

        public KeyCode[] Left => left;
        public KeyCode[] Right => right;
        public KeyCode[] Up => up;
        public KeyCode[] Down => down;
        
        public KeyCode[] Hand => hand;
        public KeyCode[] Leg => leg;
        public KeyCode[] Block => block;
        
        public bool IsPressed(IReadOnlyCollection<KeyCode> codes)
        {
            return codes.Any(UnityEngine.Input.GetKey);
        }
        public bool IsPressedDown(IReadOnlyCollection<KeyCode> codes)
        {
            return codes.Any(UnityEngine.Input.GetKeyDown);
        }
        public bool IsPressedUp(IReadOnlyCollection<KeyCode> codes)
        {
            return codes.Any(UnityEngine.Input.GetKeyUp);
        }
    }
}