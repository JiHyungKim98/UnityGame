using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class Item : MonoBehaviour
{
    private void OnMouseDown()
    {
        this.gameObject.SetActive(false);
    }

}
