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
        private GameObject levelObject;

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
            levelObject = Instantiate(loadedLevel.Level, transform);
            cam.EnableCinematic(loadedLevel.CamPos, loadedLevel.CamRot, true);
        }
        private void UnloadLevel()
        {
            PoolObjects.EnqueueAll();
            Destroy(levelObject);
            loadedLevel = null;
        }
        private void SetBullet()
        {
            bullet.position = loadedLevel.BulletPos;
            bullet.localEulerAngles = Vector3.zero;
        }
    }
}