using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject NPCName;
    public GameObject player;
    private void OnMouseDown()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        NPCName.transform.LookAt(player.transform);
    }
}
