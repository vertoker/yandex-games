using Scripts.Location.Extraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Scripts.Game.Controller
{
    [RequireComponent(typeof(Rigidbody), typeof(PersonAnimatorController))]
    public class PersonController : MonoBehaviour
    {
        [SerializeField] private float speed;

        [SerializeField] private ToolHandler handler;
        private PersonAnimatorController animator;
        private Rigidbody rb;
        private Transform tr;

        private void Awake()
        {
            animator = GetComponent<PersonAnimatorController>();
            rb = GetComponent<Rigidbody>();
            tr = GetComponent<Transform>();
        }

        public void Move(Vector2 direction)
        {
            animator.Move(direction, speed);
            direction *= speed;
            rb.velocity = new Vector3(direction.x, rb.velocity.y, direction.y);
            tr.eulerAngles = new Vector3(0f, 90f - Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, 0f);
        }
        public void StartAction(AnimationKey key)
        {
            animator.StartAction(key);
        }
        public void EndAction()
        {
            animator.EndAction();
        }
        public void SwitchTool(ToolType type = ToolType.Empty, ToolMaterial material = ToolMaterial.Wood)
        {
            handler.SwitchTool(type, material);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PersonController))]
    class PersonControllerSpawner : Editor
    {
        ToolType type = ToolType.Empty;
        ToolMaterial material = ToolMaterial.Wood;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            PersonController data = (PersonController)target;

            EditorGUILayout.Space(30);
            type = (ToolType)EditorGUILayout.EnumPopup(type);
            material = (ToolMaterial)EditorGUILayout.EnumPopup(material);
            if (GUILayout.Button("Switch Weapon"))
            {
                data.SwitchTool(type, material);
            }
        }
    }
#endif
}