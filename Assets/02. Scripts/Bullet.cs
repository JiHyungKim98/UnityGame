using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;
    private Player player;
    private float bulletSpeed = 0.1f;
    Vector3 dir;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent("Player") as Player;
        dir = new Vector3(0,0,0);
        
        Debug.Log("Awake Dir"+dir);
    }
    public void Shoot()
    {
        GameObject gun = GameObject.Find("Gun");
        dir = (this.transform.position-gun.transform.position);

    }
    private void Update()
    {
        Debug.Log("Dir"+dir);
        this.transform.position = Vector3.MoveTowards(this.transform.position, dir,bulletSpeed);
            //new Vector3(dir.x,dir.y,dir.z);
        //GetComponent<Rigidbody>().AddForce(Vector3.dir * bulletSpeed);
    }
}
