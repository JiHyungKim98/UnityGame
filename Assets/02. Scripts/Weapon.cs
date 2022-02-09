using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using ZombieWorld;

public class Weapon : MonoBehaviour
{
    public GameObject WeaponStatUI;
    public GameObject MainWeaponImg;
    public GameObject WeaponController;
    public GameObject WeaponContainerObj;
    WeaponContainer WeaponContainer;
    public GameObject inventoryObj;
    public Inventory inventory;


    [FormerlySerializedAs("weapon")] public WeaponContainer weaponContainer;

    private void OnEnable()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        inventory = inventoryObj.GetComponent<Inventory>();
    }
    private void Start()
    {
        WeaponContainer = WeaponContainerObj.GetComponent<WeaponContainer>();
    }

    private void OnMouseDown()
    {
        this.gameObject.SetActive(false);
        WeaponStatUI.SetActive(true);
        WeaponContainer._weapons.Add(this);
        if (this.gameObject.name == "Gun")
        {
            Debug.Log("gumImg");
            WeaponStatUI.transform.GetChild(0).gameObject.GetComponent<Image>().sprite= 
                        WeaponController.GetComponent<WeaponController>()._weaponImg.Find(x => x.name == "gunImg");
            WeaponStatUI.transform.GetChild(1).gameObject.GetComponent<Text>().text = 
                        "Power\n+10\nAttack Speed\n+5\nAttack Distance\n+50";
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            inventory.AddToSlot(this.gameObject);

        }
        else if(this.gameObject.name == "Paddle")
        {
            Debug.Log("paddleImg");
            WeaponStatUI.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = WeaponController.GetComponent<WeaponController>()._weaponImg.Find(x => x.name == "paddleImg");
            WeaponStatUI.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Power\n+2\nAttack Speed\n+5\nAttack Distance\n+5";
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            inventory.AddToSlot(this.gameObject);
        }
        else
        {
            Debug.Log("Img Error");
        }

    }
}
