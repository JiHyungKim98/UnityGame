using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _02._Scripts;

public class NPC : NPCController
{
    private void OnMouseDown()
    {
        if(QuestManager.Instance.GetQuestState(QuestManager.QuestType.FindBoat))
        {
            popUp.SetConversationUI(gameObject,1);
        }
        else
        {
            popUp.SetConversationUI(gameObject, 2);
        }
    }
}
