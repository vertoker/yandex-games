using Data;
using UnityEngine;
using YG;

namespace UI
{
    public class UpgradeSubscriber : MonoBehaviour
    {
        [SerializeField] private IntParam bulletCount;
        [SerializeField] private FloatParam bulletRadiusPower;
        [SerializeField] private FloatParam bulletFuelTime;
        [SerializeField] private Transform content;
        
        private UpgradeItem[] items;
        private const int Length = 3;

        private void Awake()
        {
            items = new UpgradeItem[Length];
            for (int i = 0; i < Length; i++)
                items[i] = content.GetChild(i).GetComponent<UpgradeItem>();
            
            items[0].InitializeItem(bulletCount.Length);
            items[1].InitializeItem(bulletRadiusPower.Length);
            items[2].InitializeItem(bulletFuelTime.Length);
        }
        
        public void UpdateItems()
        {
            int tempInt = YandexGame.savesData.bulletCount;
            items[0].UpdateItem(tempInt, bulletCount.GetNextValue(tempInt), bulletCount.GetPrice(tempInt), bulletCount.GetIndex(tempInt));
            
            float tempFloat = YandexGame.savesData.bulletExplosionRadiusPower;
            items[1].UpdateItem(tempFloat, bulletRadiusPower.GetNextValue(tempFloat), bulletRadiusPower.GetPrice(tempFloat), bulletRadiusPower.GetIndex(tempFloat));
            
            tempFloat = YandexGame.savesData.fuelTime;
            items[2].UpdateItem(tempFloat, bulletFuelTime.GetNextValue(tempFloat), bulletFuelTime.GetPrice(tempFloat), bulletFuelTime.GetIndex(tempFloat));
        }

        public void Upgrade(int id)
        {
            print(id);
            switch (id)
            {
                case 0:
                    if (YandexGame.savesData.money < bulletCount.GetPrice(YandexGame.savesData.bulletCount)) return;
                    if (bulletCount.GetIndex(YandexGame.savesData.bulletCount) < bulletCount.Length - 1)
                    {
                        YandexGame.savesData.bulletCount = bulletCount.GetNextValue(YandexGame.savesData.bulletCount);
                        YandexGame.SaveProgress();
                    }
                    break;
                case 1:
                    if (YandexGame.savesData.money < bulletRadiusPower.GetPrice(YandexGame.savesData.bulletExplosionRadiusPower)) return;
                    if (bulletRadiusPower.GetIndex(YandexGame.savesData.bulletExplosionRadiusPower) < bulletRadiusPower.Length - 1)
                    {
                        YandexGame.savesData.bulletExplosionRadiusPower = bulletRadiusPower.GetNextValue(YandexGame.savesData.bulletExplosionRadiusPower);
                        YandexGame.SaveProgress();
                    }
                    break;
                case 2:
                    if (YandexGame.savesData.money < bulletFuelTime.GetPrice(YandexGame.savesData.fuelTime)) return;
                    if (bulletFuelTime.GetIndex(YandexGame.savesData.fuelTime) < bulletFuelTime.Length - 1)
                    {
                        YandexGame.savesData.fuelTime = bulletFuelTime.GetNextValue(YandexGame.savesData.fuelTime);
                        YandexGame.SaveProgress();
                    }
                    break;
            }
            UpdateItems();
        }

        public int GetFullSpendMoney()
        {
            int money = bulletCount.GetSpendMoney(YandexGame.savesData.bulletCount);
            money += bulletRadiusPower.GetSpendMoney(YandexGame.savesData.bulletExplosionRadiusPower);
            money += bulletFuelTime.GetSpendMoney(YandexGame.savesData.fuelTime);
            return money;
        }
    }
}
