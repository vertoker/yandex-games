using System;
using Data;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using YG;

namespace Game
{
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField] private LevelData[] levels;
        [SerializeField] private CameraFollower cam;
        [SerializeField] private Transform bullet;
        
        private LevelData loadedLevel;
        private Transform levelTransform;

        private void OnEnable()
        {
            GameCycle.StartMenuEvent += LoadLevel;
            GameCycle.StartCycleEvent += SetBullet;
        }
        private void OnDisable()
        {
            GameCycle.StartMenuEvent -= LoadLevel;
            GameCycle.StartCycleEvent -= SetBullet;
        }

        private void LoadLevel()
        {
            if (loadedLevel)
                UnloadLevel();
            loadedLevel = levels[YandexGame.savesData.currentLevel - 1];
            levelTransform = Instantiate(loadedLevel.Level, transform).transform;
            cam.EnableCinematic(loadedLevel.CamPos, loadedLevel.CamRot, true);
        }
        private void UnloadLevel()
        {
            for (int i = 0; i < levelTransform.childCount; i++)
                PoolObjects.RemoveFromActive(levelTransform.GetChild(i).GetComponent<DestructableObject>());
            PoolObjects.EnqueueAll();
            Destroy(levelTransform.gameObject);
            loadedLevel = null;
        }
        private void SetBullet()
        {
            bullet.position = loadedLevel.BulletPos;
            bullet.localEulerAngles = Vector3.zero;
        }
    }
}