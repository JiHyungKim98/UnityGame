using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : NPCController
{
    
    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown()");
        popUp.SetConversationUI(this.gameObject);
    }
    
}
