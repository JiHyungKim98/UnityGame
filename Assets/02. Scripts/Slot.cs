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
    public WeaponContainer WeaponContainer;


    private void Start()
    {
        inventory = GetComponentInParent<Inventory>();
        
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
                    inventory.Heal(50);
                    break;
                case "Bullet":
                    //player.Heal(50);
                    break;
                case "Map":
                    inventory.ShowMap();
                    break;
                case "Bandage":
                    inventory.Heal(20);
                    break;
                case "Candle":
                    break;

            }
            //else if (slotObj.transform.GetChild(0).gameObject.tag == "Item")
            //{
            //    if (slotObj.transform.GetChild(0).name == "MedicBag")
            //    {
            //        player.Heal(50);
            //    }
            //    else if (slotObj.transform.GetChild(0).name == "Bandage")
            //    {
            //        player.Heal(20);
            //    }
            //    else if (slotObj.transform.GetChild(0).name == "Map")
            //    {
            //        RealMap.SetActive(true);
            //    }
            //    else if (slotObj.transform.GetChild(0).name == "Bullet")
            //    {

            //    }
            //    else if (slotObj.transform.GetChild(0).name == "Candle")
            //    {

            //    }
        }
        else
        {
            Debug.Log("Slot.cs UseItem() err");
        }
        //if (slotObj.transform.GetChild(0).gameObject.tag == "Weapon")
        //{
        //    if (MainWeapon.transform.childCount == 0) // MainWeapon is null
        //    {
        //        slotObj.GetComponent<Image>().sprite = null;
        //        slotObj.transform.GetChild(0).transform.SetParent(MainWeapon.transform);
        //    }
        //    else // already have MainWeapon
        //    {
        //        MainWeapon.transform.GetChild(0).SetParent(tmpObj.transform);


        //        slotObj.transform.GetChild(0).transform.SetParent(MainWeapon.transform);

        //        tmpObj.transform.GetChild(0).SetParent(slotObj.transform);

        //        slotObj.GetComponent<Image>().sprite = transform.GetComponentInParent<Inventory>().SetSprite(slotObj.transform.GetChild(0).gameObject);

        //        slotObj.transform.GetChild(0).gameObject.SetActive(false);

        //    }

        //    // Current Weapon sprite change
        //    CurrentWeapon.GetComponent<Image>().sprite = transform.GetComponentInParent<Inventory>().SetSprite(MainWeapon.transform.GetChild(0).gameObject);

        //    MainWeapon.transform.GetChild(0).gameObject.SetActive(true);
        //    MainWeapon.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 0, 0);
        //    if (MainWeapon.transform.GetChild(0).gameObject.name == "Gun")
        //    {
        //        MainWeapon.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Euler(new Vector3(180, -90, -90));
        //    }
        //    else
        //    {
        //        MainWeapon.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        //    }
        //}
        //else if (slotObj.transform.GetChild(0).gameObject.tag == "Item")
        //{
        //    if (slotObj.transform.GetChild(0).name == "MedicBag")
        //    {
        //        player.Heal(50);
        //    }
        //    else if (slotObj.transform.GetChild(0).name == "Bandage")
        //    {
        //        player.Heal(20);
        //    }
        //    else if (slotObj.transform.GetChild(0).name == "Map")
        //    {
        //        RealMap.SetActive(true);
        //    }
        //    else if (slotObj.transform.GetChild(0).name == "Bullet")
        //    {

        //    }
        //    else if (slotObj.transform.GetChild(0).name == "Candle")
        //    {

        //    }
        //}
        //else
        //{

        //}
    }
}
