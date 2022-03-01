using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;
    private Player player;
    private WeaponContainer weaponContainer;
    private float bulletSpeed = 10f;
    public static Vector3 dir;
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        weaponContainer = GameObject.FindWithTag("WeaponContainer").GetComponent<WeaponContainer>();
        StartCoroutine(BulletLifeTime());
        
    }

    IEnumerator BulletLifeTime()
    {
        GetComponent<Rigidbody>().AddForce(player.transform.forward * bulletSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }

}
