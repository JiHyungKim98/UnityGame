using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;
    private Player player;
    private float bulletSpeed = 5f;
    public static Vector3 dir;

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        GetComponent<Rigidbody>().AddForce(player.transform.forward * bulletSpeed, ForceMode.Impulse);
    }
    
}
