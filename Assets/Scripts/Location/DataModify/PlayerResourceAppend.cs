using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Scripts.Game.Data;
using YG;

namespace Scripts.Location.DataModify
{
    public class PlayerResourceAppend : MonoBehaviour
    {
        [SerializeField] private PlayerResourceType type;
        [SerializeField] private ulong resource;
        private Action<PlayerResourceType, ulong> appendEvent;

        private void Awake()
        {
            appendEvent = YandexGame.savesData.resources.AppendValue;
        }
        public void InvokeAppend()
        {
            appendEvent?.Invoke(type, resource);
        }
    }
}