using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{
    public Weapon weapon;
    private void Awake()
    {
        weapon = GameObject.Find("Weapon").GetComponent("Weapon") as Weapon;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            weapon.MonsterAttack(collision.gameObject.GetComponent<Bullet>());
        }
    }
}
