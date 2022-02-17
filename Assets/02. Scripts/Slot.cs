using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;
public class Slot : MonoBehaviour
{
    public Inventory inventory;
    public GameObject thisItem;
    public GameObject CurrentWeapon;
    public GameObject ConfirmUI;
    public WeaponContainer WeaponContainer;
    public Button thisSlot;
   

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
            ConfirmUI.SetActive(true);
        }
    }

    public void UseItem()
    {
        thisItem = gameObject.transform.GetChild(0).gameObject;
        if (thisItem.GetComponent<Weapon>()==true)
        {
            WeaponContainer.SetMainWeapon(thisItem,this.gameObject);
        }
        else if(thisItem.GetComponent<Item>() == true)
        {
            switch (thisItem.name)
            {
                case "MedicBag":
                    inventory.Dump(thisItem);
                    inventory.Heal(50);
                    break;
                case "Bullet":
                    //player.Heal(50);
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
