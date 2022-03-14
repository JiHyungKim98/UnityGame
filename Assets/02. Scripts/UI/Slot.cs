using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;
using _02._Scripts;

public class Slot : MonoBehaviour
{
    public Inventory inventory;
    public GameObject thisItem;
    public GameObject CurrentWeapon;
    public GameObject ConfirmUI;
    public WeaponContainer WeaponContainer;
    public Button thisSlot;
    
    public Quest quest;
    

    private void Start()
    {
        inventory = GetComponentInParent<Inventory>();
        thisSlot.onClick.AddListener(ShowConfirmUI);
    }

    public void ShowConfirmUI()
    {
        if (this.GetComponent<Image>().sprite == null)
        {
            return;
        }
        else
        {
            ConfirmUI.gameObject.SetActive(true);
        }
    }

    public void UseItem()
    {
        
        thisItem = gameObject.transform.GetChild(0).gameObject;
        if (thisItem.GetComponent<Weapon>()==true)
        {

            if (!quest.isFirstAtk)
            {
                QuestManager.Instance.Notify(QuestManager.QuestType.UseWeapon);
                quest.isFirstAtk = true;
            }
            WeaponContainer.SetMainWeapon(thisItem,this.gameObject);
        }
        else if(thisItem.GetComponent<Item>() == true)
        {
            if (!quest.isFirstUseItem)
            {
                QuestManager.Instance.Notify(QuestManager.QuestType.UseItem);
                quest.isFirstAtk = true;
            }
            switch (thisItem.name)
            {
                case "MedicBag":
                    inventory.Dump(thisItem);
                    inventory.Heal(50);
                    break;
                case "Bullet":
                    break;
                case "Map":
                    inventory.ShowMap();
                    break;
                case "Bandage":
                    inventory.Dump(thisItem);
                    inventory.Heal(20);
                    break;
                case "Candle":
                    break;

            }
            
        }
        else
        {
            Debug.Log("Slot.cs UseItem() err");
        }
        
    }
}
