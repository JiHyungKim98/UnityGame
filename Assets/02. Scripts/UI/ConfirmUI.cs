using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class ConfirmUI : MonoBehaviour
{
    public GameObject slotObj;
    public Button useBtn, dumpBtn;
    public Inventory inventory;

    private void Start()
    {
        useBtn.onClick.AddListener(ClickUse);
        dumpBtn.onClick.AddListener(ClickDump);
        inventory = GetComponentInParent<Inventory>();
        
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
        GameObject thisItem = slotObj.transform.GetChild(0).gameObject;
        inventory.Dump(thisItem);
    }
}
