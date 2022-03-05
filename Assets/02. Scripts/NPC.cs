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
        this.transform.LookAt(player.transform);
    }
}
