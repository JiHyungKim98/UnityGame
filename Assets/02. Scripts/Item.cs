using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class Item : ItemController
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
