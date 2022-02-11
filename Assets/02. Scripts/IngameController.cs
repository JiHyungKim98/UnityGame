using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameController : MonoBehaviour
{
    
    public Inventory inventory;
    public GameObject ElementStatUI;
    WeaponController WeaponController;
    ItemController ItemController;
    Text changeText;
    Image changeImg;

    private void Start()
    {
        WeaponController = GetComponentInChildren<WeaponController>();
        ItemController = GetComponentInChildren<ItemController>();
        changeImg = ElementStatUI.GetComponentInChildren<Image>();
        changeText = ElementStatUI.transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    protected void MouseOn(GameObject obj)
    {
        obj.SetActive(false);
        Debug.Log("obj.tag"+obj.tag.ToString());
        Debug.Log("obj.name"+obj.name.ToString());

        if (obj.tag == "Weapon")
        {
            if (obj.name == "Gun")
            {

                //slot.GetComponent<Image>().sprite = _item.Find(x => x.name == "BandageImg");
                changeImg.sprite =WeaponController._weaponImg.Find(x => x.name == "GunImg");
                changeText.text ="Power\n+5\nAttack Speed\n+5\nAttack Distance\n+50";
            }
            else if (obj.name == "Paddle")
            {
                changeImg.sprite = WeaponController._weaponImg.Find(x => x.name == "PaddleImg");
                changeText.text = "Power\n+2\nAttack Speed\n+5\nAttack Distance\n+5";
            }
        }

        else if (obj.tag == "Item")
        {
            if (obj.name == "MedicBag")
            {
                changeImg.sprite = ItemController._itemImg.Find(x => x.name == "MedicBagImg");
                changeText.text ="Heal\n+50";
            }
            else if (obj.name == "Candle")
            {
                changeImg.sprite = ItemController._itemImg.Find(x => x.name == "CandleImg");
                changeText.text = "Bright to use";
            }
            else if (obj.name == "Bullet")
            {
                changeImg.sprite = ItemController._itemImg.Find(x => x.name == "BulletImg");
                changeText.text = "Shoot the gun with this";
            }
            else if (obj.name == "Map")
            {
                changeImg.sprite = ItemController._itemImg.Find(x => x.name == "MapImg");
                changeText.text = "Show Map";
            }
            else if (obj.name == "Bandage")
            {
                changeImg.sprite = ItemController._itemImg.Find(x => x.name == "BandageImg");
                changeText.text = "Heal\n+20";
            }
        }
        else
        {
            Debug.Log("Img Error");
        }

        ElementStatUI.SetActive(true);
        inventory.AddToSlot(obj);
        obj.GetComponent<BoxCollider>().enabled = false;

    }
}
