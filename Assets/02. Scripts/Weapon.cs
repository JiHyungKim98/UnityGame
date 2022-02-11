using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using ZombieWorld;

public class Weapon : WeaponController
{
    private void OnEnable()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    

    private void OnMouseDown()
    {
        MouseOn(this.gameObject);
        
    }
}
