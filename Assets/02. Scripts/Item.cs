using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class Item : MonoBehaviour
{
    public Inventory inventory;

    private void OnMouseDown()
    {
        this.gameObject.SetActive(false);
        inventory.SetStatUI(this.gameObject);
        inventory.AddToSlot(this.gameObject);
        GetComponent<BoxCollider>().enabled = false;
    }

}
