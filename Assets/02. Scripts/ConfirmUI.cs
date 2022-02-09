using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmUI : MonoBehaviour
{
    public GameObject slotObj;
    public Button useBtn, dumpBtn;
    public GameObject CurrentWeapon;
    public GameObject MainWeapon;
    public GameObject tmpObj;

    private void Start()
    {
        useBtn.onClick.AddListener(Use);
        dumpBtn.onClick.AddListener(Dump);
    }

    public void GetGameObject(GameObject obj)
    {
        slotObj = obj;
    }

    public void Use()
    {
        if (slotObj.transform.GetChild(0).gameObject.tag=="Weapon")
        {
            CurrentWeapon.GetComponent<Image>().sprite = slotObj.GetComponent<Image>().sprite; // Current Weapon sprite change

            if (MainWeapon.transform.childCount == 0) // MainWeapon is null
            {
                slotObj.GetComponent<Image>().sprite = null;
                slotObj.transform.GetChild(0).transform.SetParent(MainWeapon.transform);
            }
            else // already have MainWeapon
            {
                Debug.Log("잘해보자");
                MainWeapon.transform.GetChild(0).SetParent(tmpObj.transform);
                slotObj.GetComponent<Image>().sprite = null;
                slotObj.transform.GetChild(0).transform.SetParent(MainWeapon.transform);
                tmpObj.transform.GetChild(0).SetParent(slotObj.transform);

            }
            
            
            MainWeapon.transform.GetChild(0).gameObject.SetActive(true);
            MainWeapon.transform.GetChild(0).gameObject.transform.localPosition=new Vector3(0, 0, 0);
            if (MainWeapon.transform.GetChild(0).gameObject.name == "Gun")
            {
                MainWeapon.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Euler(new Vector3(180 ,-90, -90));
            }
            else
            {
                MainWeapon.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

            }
        }
        else
        {
            Debug.Log("틀렸음");
        }
    }

    public void Dump()
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
