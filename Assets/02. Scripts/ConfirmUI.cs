using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class ConfirmUI : MonoBehaviour
{
    public GameObject slotObj;
    public Button useBtn, dumpBtn;
    public GameObject CurrentWeapon;
    public GameObject MainWeapon;
    
    public Player player;
    public GameObject RealMap;
    public Inventory inventory;

    private void Start()
    {
        useBtn.onClick.AddListener(ClickUse);
        dumpBtn.onClick.AddListener(ClickDump);
    }

    public void GetGameObject(GameObject obj)
    {
        slotObj = obj;
    }

    public void ClickUse()
    {
        slotObj.GetComponent<Slot>().UseItem();
    }

    public void ClickDump()
    {
        slotObj.GetComponent<Image>().sprite = null;
        if (slotObj.transform.childCount > 0) // child O
        {
            slotObj.transform.GetChild(0).gameObject.SetActive(true);
            slotObj.transform.GetChild(0).SetParent(transform.root.parent);

        }
        else // child X
        {

        }
    }
}
