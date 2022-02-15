using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using ZombieWorld;

public class Weapon : MonoBehaviour
{
    public Inventory inventory;
    
    private void OnMouseDown()
    {
        gameObject.SetActive(false);
        inventory.SetStatUI(this.gameObject);
        inventory.AddToSlot(this.gameObject);
        GetComponent<BoxCollider>().enabled = false;

    }
}
