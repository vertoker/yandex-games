using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Scripts.Game
{
    public static class SaveSystem
    {
        public static Action updateResources;

        public static void Save()
        {
            YandexGame.SaveProgress();
            updateResources?.Invoke();
        }
    }
}