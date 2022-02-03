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
    public bool isNear;
    public Inventory inventory;

    private void Awake()
    {
        ThisItem = transform.GetChild(0).gameObject;
    }
    public void IsNear()
    {
        //Debug.Log("this item:", gameObject.name);
        //if (Vector3.Distance(this.transform.position, player.transform.position) <= 5.0)
        if(isNear)
        {
            weapon._weapons.Add(gameObject);
            if (weapon.transform.GetChild(0).transform.childCount == 0) // MainWeapon Child Null
            {
                transform.parent = weapon.transform.GetChild(0).transform;
                inventory.WeaponSetUI(0);

            }
            else if(weapon.transform.GetChild(0).transform.childCount == 1) // MainWeapon Child 1
            {
                transform.parent = weapon.transform.Find("SubWeapon").transform;
                inventory.WeaponSetUI(1);
                //this.gameObject.SetActive(false);
            }

            // Change to parent position
            transform.position = new Vector3(transform.parent.transform.position.x
                                                    , transform.parent.transform.position.y
                                                    , transform.parent.transform.position.z);

            // Item explain canvas
            Destroy(ThisItem.gameObject);
            isLive = true;

            // Change to parent rotation
            transform.rotation = Quaternion.Euler(new Vector3(transform.parent.transform.position.x
                                                                    , transform.parent.transform.position.y
                                                                    , transform.parent.transform.position.z));

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
            if (Vector3.Distance(transform.position, player.transform.position) <= 5.0)
            {
                isNear = true;
                //Debug.Log("WeaponChild");
                ThisItem.gameObject.transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
                ThisItem.gameObject.SetActive(true);
            }
            else
            {
                isNear = false;
                ThisItem.gameObject.SetActive(false);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
