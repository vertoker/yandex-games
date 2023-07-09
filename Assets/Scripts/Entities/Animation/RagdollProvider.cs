using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Entities.Animation
{
    public class RagdollProvider : MonoBehaviour
    {
        [SerializeField] private bool active = false;
        [Space]
        [SerializeField] private Transform pelvis;
        [SerializeField] private Transform middleSpine;
        [SerializeField] private Transform head;
        [Space]
        [SerializeField] private Transform leftHips;
        [SerializeField] private Transform leftKnee;
        [Space]
        [SerializeField] private Transform rightHips;
        [SerializeField] private Transform rightKnee;
        [Space]
        [SerializeField] private Transform leftArm;
        [SerializeField] private Transform leftElbow;
        [Space]
        [SerializeField] private Transform rightArm;
        [SerializeField] private Transform rightElbow;

        private Rigidbody[] _rbs;

        private void Awake()
        {
            _rbs = new []
            {
                pelvis.GetComponent<Rigidbody>(),
                middleSpine.GetComponent<Rigidbody>(),
                head.GetComponent<Rigidbody>(),
                
                leftHips.GetComponent<Rigidbody>(),
                leftKnee.GetComponent<Rigidbody>(),
                
                rightHips.GetComponent<Rigidbody>(),
                rightKnee.GetComponent<Rigidbody>(),
                
                leftArm.GetComponent<Rigidbody>(),
                leftElbow.GetComponent<Rigidbody>(),
                
                rightArm.GetComponent<Rigidbody>(),
                rightElbow.GetComponent<Rigidbody>()
            };
            
            if (active)
                EnableRagdoll();
            else
                DisableRagdoll();
        }

        public void EnableRagdoll()
        {
            active = true;
            
            foreach (var rb in _rbs)
            {
                rb.isKinematic = false;
            }
        }
        public void DisableRagdoll()
        {
            active = false;
            
            foreach (var rb in _rbs)
            {
                rb.isKinematic = true;
            }
        }
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(RagdollProvider))]
    public class RagdollProviderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var provider = (RagdollProvider)target;
            if (provider == null) return;

            if (GUILayout.Button("Enable"))
                provider.EnableRagdoll();
            if (GUILayout.Button("Disable"))
                provider.DisableRagdoll();
            
            base.OnInspectorGUI();
        }
    }
#endif
}