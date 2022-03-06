using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;
public class Letter : MonoBehaviour
{
    public PopUp popup;
    private void OnMouseDown()
    {
        popup.Show(this.gameObject);
    }
}
