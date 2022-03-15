using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;
using _02._Scripts;

public class Quest : MonoBehaviour
{
    [SerializeField] private List<GameObject> _quest = new List<GameObject>();
   
    public bool isFirstAtk;
    public bool isFirstUseItem;
    public bool isFindBoat;
    private void Update()
    {
        if (QuestManager.Instance.GetQuestState(QuestManager.QuestType.UseWeapon) && isFirstAtk==true)
        {
            _quest[0].gameObject.GetComponent<Toggle>().isOn = true;
        }
        if(QuestManager.Instance.GetQuestState(QuestManager.QuestType.UseWeapon) && isFirstUseItem == true)
        {
            _quest[1].gameObject.GetComponent<Toggle>().isOn = true;
        }
        if(QuestManager.Instance.GetQuestState(QuestManager.QuestType.FindBoat)&&!isFindBoat)
        {
            isFindBoat = true;
            _quest[6].gameObject.GetComponent<Toggle>().isOn = true;
        }
    }
    

}
