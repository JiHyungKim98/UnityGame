using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class Bandage : MonoBehaviour
{
    public GameObject player;
    public GameObject Item;


    private void Awake()
    {
        Item = transform.GetChild(0).gameObject;
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 5.0)
        {
            Item.gameObject.transform.rotation = Quaternion.LookRotation(this.transform.position - player.transform.position);
            Item.gameObject.SetActive(true);
        }
        else
        {
            Item.gameObject.SetActive(false);
        }
    }

    public void IsNear()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 5.0)
            this.gameObject.SetActive(false);
        
    }
}
