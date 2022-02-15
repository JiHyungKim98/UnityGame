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
    public Player player;
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
            Debug.Log("Slot count over!");
        }
        else
        {
            
            foreach(GameObject slot in _slots)
            {
                if (slot.transform.childCount==0)
                {
                    obj.transform.SetParent(slot.transform);
                    slot.GetComponent<Image>().sprite = obj.name switch
                    {
                        "Gun" => masterData.GetItem(Items.Gun).thumbnail,
                        "Paddle" => masterData.GetItem(Items.Paddle).thumbnail,
                        "MedicBag" => masterData.GetItem(Items.MedicBag).thumbnail,
                        "Candle" => masterData.GetItem(Items.Candle).thumbnail,
                        "Map" => masterData.GetItem(Items.Map).thumbnail,
                        "Bullet" => masterData.GetItem(Items.Bullet).thumbnail,
                        "Bandage" => masterData.GetItem(Items.Bandage).thumbnail,
                        _ => slot.GetComponent<Image>().sprite
                    };
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

                //return _weapon.Find(x => x.name == "GunImg");
        }
    }    
    
    public string SetTxt(GameObject obj)
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
        StatTxt.GetComponent<Text>().text = SetTxt(obj);
    }

    public void ShowMap()
    {
        RealMap.SetActive(true);
    }
    public void Heal(int num)
    {
        player.Heal(num);
    }


  
   




}
