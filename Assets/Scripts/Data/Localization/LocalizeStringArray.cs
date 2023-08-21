using Core.Language;
using UnityEngine;

namespace Data.Localization
{
    [CreateAssetMenu(menuName = "Presets/Localization/" + nameof(LocalizeStringArray), fileName = nameof(LocalizeStringArray))]
    public class LocalizeStringArray : BaseLocalize
    {
        [SerializeField] private string[] ru;
        [SerializeField] private string[] en;
        [SerializeField] private string[] tr;
        
        public override string GetString()
        {
            var data = GetArrayLanguage();
            return data == null ? string.Empty : data[Random.Range(0, data.Length)];
        }

        private string[] GetArrayLanguage()
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