using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Scripts.Tools;

namespace Scripts.Location.Utility
{
    public class TransformMover : MonoBehaviour
    {
        [SerializeField] private List<Vector3> positions;
        [SerializeField] private int currentTargetPosition = 0;
        [SerializeField] private float speed = 0.1f;
        [SerializeField] private bool beginOnStart = true;
        private Coroutine updater;
        private Transform _self;

        private void Awake()
        {
            _self = GetComponent<Transform>();
        }
        private void Start()
        {
            if (beginOnStart)
                StartAnim();
        }
        public void StartAnim()
        {
            updater = StartCoroutine(Updater());
        }
        public void EndAnim()
        {
            StopCoroutine(updater);
        }

        private IEnumerator Updater()
        {
            while (true)
            {
                var nextPosition = Vector3.MoveTowards(_self.position, positions[currentTargetPosition], speed * Time.timeScale);
                if (_self.position == nextPosition)
                {
                    currentTargetPosition++;
                    if (currentTargetPosition == positions.Count)
                        currentTargetPosition = 0;
                }
                _self.position = nextPosition;
                yield return new WaitForFixedUpdate();
            }
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(TransformMover))]
        public class PlatformMovementEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                TransformMover platform = (TransformMover)target;

                if (GUILayout.Button("Add point"))
                {
                    var obj = Selection.activeGameObject;
                    if (obj != null)
                        platform.positions.Add(obj.transform.position);
                }

                base.OnInspectorGUI();
            }
        }
#endif
    }
}