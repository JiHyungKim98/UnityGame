using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;

public class WeaponChild : MonoBehaviour
{
    public GameObject ThisItem;
    public Weapon weapon;
    public Player player;
    public bool isLive;
    private void Awake()
    {
        ThisItem = transform.GetChild(0).gameObject;
        weapon = GameObject.Find("WeaponController").GetComponent("Weapon") as Weapon;
        player = GameObject.Find("Player").GetComponent("Player") as Player;
    }
    public void IsNear()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 5.0)
        {
            //Debug.Log("Weapon Cnt" + weapon.weaponCnt);
            weapon._weapons.Add(this.gameObject);
            //weapon.weaponCnt = 1;
            if (GameObject.Find("WeaponController").transform.GetChild(0).transform.childCount == 0)
            {
                this.transform.parent = GameObject.Find("WeaponController").transform.GetChild(0).transform;
                
            }
            else if(GameObject.Find("WeaponController").transform.GetChild(0).transform.childCount == 1)
            {
                this.transform.parent = GameObject.Find("WeaponController").transform.GetChild(2).transform;
            }
            this.transform.position = new Vector3(this.transform.parent.transform.position.x
                                                    , this.transform.parent.transform.position.y
                                                    , this.transform.parent.transform.position.z);
            Destroy(ThisItem.gameObject);
            isLive = true;
            this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.parent.transform.position.x
                                                                    , this.transform.parent.transform.position.y
                                                                    , this.transform.parent.transform.position.z));

            //GameObject.Find("MainWeapon").GetComponent<Image>().
        }
        else
        {
            Debug.Log("IsNear Fail");
        }


    }

    void FixedUpdate()
    {
        if (!isLive)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) <= 5.0)
            {
                //Debug.Log("WeaponChild");
                ThisItem.gameObject.transform.rotation = Quaternion.LookRotation(this.transform.position - player.transform.position);
                ThisItem.gameObject.SetActive(true);
            }
            else
            {
                ThisItem.gameObject.SetActive(false);
            }
        }
        
    }
}
