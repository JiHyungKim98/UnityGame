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
    public GameObject Shooter;

    
    public void Shoot()
    {
        //Shooter = GameObject.Find("gun");
        

    }
    private void OnEnable()
    {
        Shooter = GameObject.Find("Shooter");
        player = GameObject.Find("Player").GetComponent("Player") as Player;
        //dir = (this.transform.position - player.transform.position);
        Debug.Log("Dir" + dir);
        GetComponent<Rigidbody>().AddForce(player.transform.forward * bulletSpeed, ForceMode.Impulse);
        //GetComponent<Rigidbody>().AddForce(Vector3.forward * bulletSpeed);
        //this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.forward, bulletSpeed);

    }
    private void Update()
    {
        //Debug.Log("Dir"+dir);
        //this.transform.position = Vector3.MoveTowards(this.transform.position, Shooter.transform.position*10,bulletSpeed);
            //new Vector3(dir.x,dir.y,dir.z);
        //GetComponent<Rigidbody>().AddForce(Vector3.forward * bulletSpeed);
    }
}
