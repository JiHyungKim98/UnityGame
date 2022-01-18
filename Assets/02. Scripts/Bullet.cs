using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;

public class Bullet : MonoBehaviour
{
    private Player player;
    private float bulletSpeed;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent("Player") as Player;
        bulletSpeed = 50;
        
    }
    private void Update()
    {
        Vector3 dir = player.transform.forward;
        transform.position += dir * bulletSpeed * Time.deltaTime;
    }
}
