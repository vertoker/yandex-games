using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace UI
{
    public class UpgradeItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text currentText;
        [SerializeField] private TMP_Text nextText;
        [SerializeField] private TMP_Text priceText;
        [Space]
        [SerializeField] private Image originPoint;
        [SerializeField] private Transform parentPoints;
        private Image[] points;
        [Space]
        [SerializeField] private string titleStr = "Количество пуль";
        [SerializeField] private string currentStr = "Сейчас: ";
        [SerializeField] private string nextStr = "Улучшение: ";
        [SerializeField] private string priceStr = "Цена: ";
        [SerializeField] private string fullStr = "Полностью улучшено";
        [Space]
        [SerializeField] private Color enable = new Color(1f, 1f, 1f);
        [SerializeField] private Color selected = new Color(1f, 0f, 0f);
        [SerializeField] private Color disable = new Color(0f, 0f, 0f);

        private int countUpgrades;

        public void InitializeItem(int length)
        {
            countUpgrades = length;
            points = new Image[length];
            for (int i = 0; i < length; i++)
            {
                points[i] = Instantiate(originPoint, parentPoints);
            }
        }
        
        public void UpdateItem(float current, float next, int price, int indexCurrent)
        {
            for (int i = 0; i < indexCurrent; i++)
                points[i].color = enable;
            points[indexCurrent].color = selected;
            for (int i = indexCurrent + 1; i < countUpgrades; i++)
                points[i].color = disable;

            titleText.text = titleStr;
            currentText.text = currentStr + current.ToString(CultureInfo.InvariantCulture);
            priceText.text = priceStr + price.ToString(CultureInfo.InvariantCulture);;

            if (current != next)
                nextText.text = nextStr + next.ToString(CultureInfo.InvariantCulture);
            else
                nextText.text = fullStr;
        }
    }
}
