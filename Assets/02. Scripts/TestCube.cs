using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestCube : MonoBehaviour
{
    [FormerlySerializedAs("weapon")] public WeaponContainer weaponContainer;
    private void Awake()
    {
        weaponContainer = GameObject.Find("Weapon").GetComponent("Weapon") as WeaponContainer;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            weaponContainer.MonsterAttack(collision.gameObject.GetComponent<Bullet>());
        }
    }
}
