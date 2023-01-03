using TMPro;
using UnityEngine;
using System.Linq;
using YG;

namespace UI
{
    public class MoneyUpdater : MonoBehaviour
    {
        [SerializeField] private UpgradeSubscriber moneyData;
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private string moneyStr = "Очки: ";
        
        public void MoneyUpdate()
        {
            YandexGame.savesData.money = YandexGame.savesData.levelPoints.Sum() - moneyData.GetFullSpendMoney();
            moneyText.text = moneyStr + YandexGame.savesData.money;
        }
    }
}
