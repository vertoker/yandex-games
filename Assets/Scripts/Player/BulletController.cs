using Game;
using Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

namespace Player
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private InputReceiver inputReceiver;
        [SerializeField] private Bullet bullet;
        [SerializeField] private Vector2 angleVelocity = new Vector2(3f, 3f);
        
        private Vector2 startPos = Vector2.zero;

        private void OnEnable()
        {
            GameCycle.StartCycleEvent += EnableBullet;
            GameCycle.EndFuelEvent += DisableFuelBullet;
            GameCycle.EndGameEvent += DisableBullet;
            inputReceiver.OnDownUpdate += Down;
            inputReceiver.OnDragUpdate += Drag;
            inputReceiver.OnUpUpdate += Up;
        }
        private void OnDisable()
        {
            GameCycle.StartCycleEvent -= EnableBullet;
            GameCycle.EndFuelEvent -= DisableFuelBullet;
            GameCycle.EndGameEvent -= DisableBullet;
            inputReceiver.OnDownUpdate -= Down;
            inputReceiver.OnDragUpdate -= Drag;
            inputReceiver.OnUpUpdate -= Up;
        }

        private void Down(PointerEventData data)
        {
            startPos = data.pointerCurrentRaycast.screenPosition;
            bullet.MoveAngle(0f, 0f);
        }
        private void Drag(PointerEventData data)
        {
            var offset = data.pointerCurrentRaycast.screenPosition - startPos;
            var scale = new Vector2(offset.x / Screen.width, offset.y / Screen.height) * 2f;
            bullet.MoveAngle(scale.x * angleVelocity.x, scale.y * angleVelocity.y);
        }
        private void Up(PointerEventData data)
        {
            bullet.MoveAngle(0f, 0f);
        }

        private void EnableBullet()
        {
            bullet.GetComponent<Explosion>().SetExplosionParams(YandexGame.savesData.bulletExplosionRadiusPower, YandexGame.savesData.bulletExplosionRadiusPower);
            bullet.gameObject.SetActive(true);
            bullet.SetActiveForce(true);
            
            var tr = bullet.transform;
            var angle = tr.localEulerAngles;
            tr.localEulerAngles = new Vector3(0f, angle.y, angle.z);
        }
        private void DisableFuelBullet()
        {
            bullet.SetActiveForce(false);
        }
        private void DisableBullet()
        {
            bullet.gameObject.SetActive(false);
        }
    }
}