using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour { 

    [SerializeField] private List<GameObject> _slots = new List<GameObject>();
    [SerializeField] private List<Sprite> _weapon = new List<Sprite>();

    public GameObject MainWeaponOjb;
    public GameObject SubWeaponOjb;
    public GameObject MainWeaponImg;
    public GameObject SubWeaponImg;
    public Sprite tempImg;

    //public void WeaponSetUI(int num)
    //{
    //    if (num == 0)
    //    {
    //        if (MainWeaponOjb.transform.GetChild(0).name == "gun")
    //        {
    //            MainWeaponImg.GetComponent<Image>().sprite = _weapon[0];
    //            Debug.Log("gun img");
    //        }
    //        else if (MainWeaponOjb.transform.GetChild(0).name == "Paddle")
    //        {
    //            Debug.Log("paddle img");
    //            MainWeaponImg.GetComponent<Image>().sprite = _weapon[1];

    //        }
    //        else
    //        {
    //            Debug.Log("num0 Img drawing fail");
    //        }
    //    }
    //    else
    //    {
    //        if(SubWeaponOjb.transform.GetChild(0).name == "gun")
    //        {
    //            SubWeaponImg.GetComponent<Image>().sprite = _weapon[0];
    //        }
    //        else if (SubWeaponOjb.transform.GetChild(0).name == "Paddle")
    //        {
    //            Debug.Log("paddle img");
    //            SubWeaponImg.GetComponent<Image>().sprite = _weapon[1];

    //        }
    //        else
    //        {
    //            Debug.Log("num1 Img drawing fail");
    //        }
    //    }
        
    //}

    public void WeaponChangeUI()
    {
        if (MainWeaponOjb.transform.GetChild(0).name == "gun")
        {
            MainWeaponImg.GetComponent<Image>().sprite = _weapon[1];
            SubWeaponImg.GetComponent<Image>().sprite = _weapon[0];
        }
        else
        {
            MainWeaponImg.GetComponent<Image>().sprite = _weapon[0];
            SubWeaponImg.GetComponent<Image>().sprite = _weapon[1];
        }
    }
}
