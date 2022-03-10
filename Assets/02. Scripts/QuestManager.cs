using System;
using System.Collections.Generic;
using Jjamcat.Util;
using ZombieWorld;

namespace _02._Scripts
{

    public class QuestManager : Singleton<QuestManager>
    {
        public enum QuestType
        {
            UseWeapon,
            UseItem,
        }

        private Dictionary<QuestType, bool> _questState = new Dictionary<QuestType, bool>();

        public void Notify(QuestType type)
        {
            _questState[QuestType.UseWeapon] = true;
            //_questState[QuestType.UseWeapon].gameObject.GetComponent<Toggle>().isOn = true;
        }

        public bool GetQuestState(QuestType type)
        {
            return _questState.ContainsKey(type) && _questState[type];
        }
    }
}