using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;

public class NPC : MonoBehaviour
{
    public Player player;
    public PopUp popUp;
    

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 5.0f)
        {
            this.transform.LookAt(player.transform);
        }
        
    }
}
