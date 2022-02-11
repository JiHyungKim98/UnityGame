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
        //Debug.Log(slotObj.GetComponentInChildren<Weapon>().name);
        //if (slotObj.GetComponentInChildren<Weapon>().tag=="Weapon")
        {
             
            if (MainWeapon.transform.childCount == 0) // MainWeapon is null
            {
                Debug.Log("if childCount" + MainWeapon.transform.childCount);
                slotObj.GetComponent<Image>().sprite = null;
                slotObj.transform.GetChild(0).transform.SetParent(MainWeapon.transform);
                //slotObj.GetComponentInChildren<Weapon>().transform.SetParent(MainWeapon.transform);
            }
            else // already have MainWeapon
            {                
                MainWeapon.transform.GetChild(0).SetParent(tmpObj.transform);
                //MainWeapon.GetComponentInChildren<Weapon>().gameObject.transform.SetParent(tmpObj.transform);

                slotObj.transform.GetChild(0).transform.SetParent(MainWeapon.transform);
                //slotObj.GetComponentInChildren<Weapon>().gameObject.transform.SetParent(MainWeapon.transform);

                tmpObj.transform.GetChild(0).SetParent(slotObj.transform);
                //tmpObj.GetComponentInChildren<Weapon>().gameObject.transform.SetParent(slotObj.transform);

                slotObj.GetComponent<Image>().sprite = transform.GetComponentInParent<Inventory>().SetSprite(slotObj.transform.GetChild(0).gameObject);
                //slotObj.GetComponent<Image>().sprite = transform.GetComponentInParent<Inventory>().SetSprite(slotObj.GetComponentInChildren<Weapon>().gameObject);

                slotObj.transform.GetChild(0).gameObject.SetActive(false);
                //slotObj.GetComponentInChildren<Weapon>().gameObject.SetActive(false);

            }

            // Current Weapon sprite change
            CurrentWeapon.GetComponent<Image>().sprite = transform.GetComponentInParent<Inventory>().SetSprite(MainWeapon.transform.GetChild(0).gameObject);
            //CurrentWeapon.GetComponent<Image>().sprite = transform.GetComponentInParent<Inventory>().SetSprite(MainWeapon.GetComponentInChildren<Weapon>().gameObject);

            MainWeapon.transform.GetChild(0).gameObject.SetActive(true);
            MainWeapon.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 0, 0);
            if (MainWeapon.transform.GetChild(0).gameObject.name == "Gun")
            {
                MainWeapon.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Euler(new Vector3(180, -90, -90));
            }
            else
            {
                MainWeapon.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

            }
            //MainWeapon.GetComponentInChildren<Weapon>().gameObject.SetActive(true);
            //MainWeapon.GetComponentInChildren<Weapon>().gameObject.transform.localPosition=new Vector3(0, 0, 0);
            //if (MainWeapon.GetComponentInChildren<Weapon>().gameObject.name == "Gun")
            //{
            //    MainWeapon.GetComponentInChildren<Weapon>().gameObject.transform.localRotation = Quaternion.Euler(new Vector3(180 ,-90, -90));
            //}
            //else
            //{
            //    MainWeapon.GetComponentInChildren<Weapon>().gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

            //}
        }
        else
        {
            Debug.Log("∆≤∑»¿Ω");
        }
    }

    public void Dump()
    {
        slotObj.GetComponent<Image>().sprite = null;
        if (slotObj.transform.childCount > 0) // child O
        {
            slotObj.transform.GetChild(0).gameObject.SetActive(true);
            slotObj.transform.GetChild(0).SetParent(transform.root.parent);
            //slotObj.GetComponentInChildren<Weapon>().gameObject.SetActive(true);
            //slotObj.GetComponentInChildren<Weapon>().gameObject.transform.SetParent(transform.root.parent);

        }
        else // child X
        {

        }
    }
}
