using UnityEngine;

namespace Data.Localization
{
    public abstract class BaseLocalize : ScriptableObject
    {
        public abstract string GetString();
    }
}