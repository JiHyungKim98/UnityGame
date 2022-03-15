using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;
public class NPCController : MonoBehaviour
{
    public PopUp popUp;
    public Player player;
    //private void OnMouseDown()
    //{
    //    popUp.SetConversationUI(gameObject,0);
    //}
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 10f)
            transform.LookAt(player.transform);
    }
   
}
