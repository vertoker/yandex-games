using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Level", fileName = "Level xxx")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private GameObject level;
        [SerializeField] private Vector3 camPos;
        [SerializeField] private Vector3 camRot;
        [SerializeField] private Vector3 bulletPos;

        public GameObject Level => level;
        public Vector3 CamPos => camPos;
        public Vector3 CamRot => camRot;
        public Vector3 BulletPos => bulletPos;
        
#if UNITY_EDITOR
        [CustomEditor(typeof(LevelData))]
        public class ControllerGUI : Editor
        {
            public Transform cam;
            public Transform bullet;

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                LevelData data = (LevelData)target;

                EditorGUILayout.Space(20);
                cam = EditorGUILayout.ObjectField("cam", cam, typeof(Transform), true) as Transform;
                bullet = EditorGUILayout.ObjectField("bullet", bullet, typeof(Transform), true) as Transform;
                if (GUILayout.Button("Fill Data"))
                {
                    if (cam != null)
                    {
                        data.camPos = cam.position;
                        data.camRot = cam.eulerAngles;
                    }
                    if (bullet != null)
                    {
                        data.bulletPos = bullet.position;
                    }
                }
            }
        }
#endif
    }
    
}