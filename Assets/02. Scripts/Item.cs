using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class Item : MonoBehaviour
{
    public GameObject player;
    public GameObject ThisItem;


    private void Awake()
    {
        ThisItem = transform.GetChild(0).gameObject;
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 4.0)
        {
            ThisItem.gameObject.transform.rotation = Quaternion.LookRotation(this.transform.position - player.transform.position);
            ThisItem.gameObject.SetActive(true);
        }
        else
        {
            ThisItem.gameObject.SetActive(false);
        }
    }

    public void IsNear()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 5.0)
            this.gameObject.SetActive(false);
        
    }
}
