using Core.Language;
using UnityEngine;
using YG;

namespace Data.Localization
{
    [CreateAssetMenu(menuName = "Presets/Localization/" + nameof(LocalizeString), fileName = nameof(LocalizeString))]
    public class LocalizeString : BaseLocalize
    {
        [SerializeField] private string ru;
        [SerializeField] private string en;
        [SerializeField] private string tr;
        
        public override string GetString()
        {
            return LanguageHandler.ActiveLanguage switch
            {
                "ru" => ru,
                "en" => en,
                "tr" => tr,
                _ => ru
            };
        }
    }
}