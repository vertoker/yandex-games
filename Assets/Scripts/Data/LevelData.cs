using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}