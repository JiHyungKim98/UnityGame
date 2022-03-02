using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class Inventory : MonoBehaviour { 

    [SerializeField] private List<GameObject> _slots = new List<GameObject>();

    public ItemMasterData masterData;

    public GameObject ElementStatUI;
    public GameObject WeaponImg;
    public GameObject StatTxt;
    public GameObject RealMap;
    public GameObject ItemController;
    public GameObject WeaponController;
    public Player player;
    public PopUp popUp;

    public enum Items
    {
        Gun,
        Map,
        Paddle,
        Bandage,
        MedicBag,
        Bullet,
        Candle
    }
    

    public void AddToSlot(GameObject obj)
    {
        if (_slots.Count > 16)
        {
            popUp.PopUpUI("Inventory is full");
        }
        else
        {
            
            foreach(GameObject slot in _slots)
            {
                if (slot.transform.childCount==0)
                {
                    obj.transform.SetParent(slot.transform);
                    slot.GetComponent<Image>().sprite = SetSprite(obj);
                    break;
                }
            }

        }

    }

    public Sprite SetSprite(GameObject obj)
    {
        switch (obj.name)
        {
            case "Gun":
                return masterData.GetItem(Items.Gun).thumbnail;
            case "Paddle":
                return masterData.GetItem(Items.Paddle).thumbnail;
            case "MedicBag":
                return masterData.GetItem(Items.MedicBag).thumbnail;
            case "Candle":
                return masterData.GetItem(Items.Candle).thumbnail;
            case "Bullet":
                return masterData.GetItem(Items.Bullet).thumbnail;
            case "Bandage":
                return masterData.GetItem(Items.Bandage).thumbnail;
            case "Map":
                return masterData.GetItem(Items.Map).thumbnail;
            default:
                return null;

        }
    }    
    
    public Text SetTxt(GameObject obj)
    {
        switch (obj.name)
        {
            case "Gun":
                return masterData.GetItem(Items.Gun).explain;
            case "Paddle":
                return masterData.GetItem(Items.Paddle).explain;
            case "MedicBag":
                return masterData.GetItem(Items.MedicBag).explain;
            case "Candle":
                return masterData.GetItem(Items.Candle).explain;
            case "Bullet":
                return masterData.GetItem(Items.Bullet).explain;
            case "Bandage":
                return masterData.GetItem(Items.Bandage).explain;
            case "Map":
                return masterData.GetItem(Items.Map).explain;
            default:
                return null;
        }
    }

    public void SetStatUI(GameObject obj)
    {
        ElementStatUI.SetActive(true);
        WeaponImg.GetComponent<Image>().sprite = SetSprite(obj);
        StatTxt.GetComponent<Text>().text = SetTxt(obj).text;
    }

    public void ShowMap()
    {
        RealMap.SetActive(true);
    }
    public void Heal(int num)
    {
        player.Heal(num);
    }

    public void Dump(GameObject obj)
    {
        obj.transform.parent.GetComponent<Image>().sprite = null;
        if (obj.transform.parent.childCount > 0) // child O
        {
            obj.SetActive(true);
            obj.GetComponent<BoxCollider>().enabled = true;

            if (obj.GetComponent<Weapon>() == true)
            {
                obj.transform.SetParent(WeaponController.transform);
            }
            else
            {
                obj.transform.SetParent(ItemController.transform);
            }
            
            obj.transform.position=new Vector3(player.transform.position.x,player.transform.position.y, player.transform.position.z-5f);

        }
        else // child X
        {

        }
    }


  
   




}
