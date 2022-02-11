using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameController : MonoBehaviour
{
    
    public Inventory inventory;
    public GameObject ElementStatUI;
    public WeaponController WeaponController;
    ItemController ItemController;
    public Text changeText;
    public Image changeImg;

    private void Start()
    {
        WeaponController = GetComponentInChildren<WeaponController>();
        ItemController = GetComponentInChildren<ItemController>();
        changeImg = ElementStatUI.transform.GetChild(0).gameObject.GetComponent<Image>();
        changeText = ElementStatUI.transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    protected void MouseOn(GameObject obj)
    {
        obj.SetActive(false);
        ElementStatUI.SetActive(true);
        changeImg.sprite = inventory.SetSprite(obj);
        if (obj.tag == "Weapon")
        {
            if (obj.name == "Gun")
            {
                changeText.text ="Power\n+5\nAttack Speed\n+5\nAttack Distance\n+50";
            }
            else if (obj.name == "Paddle")
            {
                changeText.text = "Power\n+2\nAttack Speed\n+5\nAttack Distance\n+5";
            }
        }

        else if (obj.tag == "Item")
        {
            if (obj.name == "MedicBag")
            {
                changeText.text ="Heal\n+50";
            }
            else if (obj.name == "Candle")
            {
                changeText.text = "Bright to use";
            }
            else if (obj.name == "Bullet")
            {
                changeText.text = "Shoot the gun with this";
            }
            else if (obj.name == "Map")
            {
                changeText.text = "Show Map";
            }
            else if (obj.name == "Bandage")
            {
                changeText.text = "Heal\n+20";
            }
        }
        else
        {
            Debug.Log("Img Error");
        }

        
        inventory.AddToSlot(obj);
        obj.GetComponent<BoxCollider>().enabled = false;

    }
}
