using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;

public class NPC : MonoBehaviour
{
    public Player player;
    public PopUp popUp;
    private void OnMouseDown()
    {
        this.transform.LookAt(player.transform);
        
        //this.gameObject.SetActive(false);
    }

    
}
