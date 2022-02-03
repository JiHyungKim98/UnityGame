using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using ZombieWorld;

public class Weapon : MonoBehaviour
{
    public GameObject ThisItem;
    [FormerlySerializedAs("weapon")] public WeaponContainer weaponContainer;

    private void Awake()
    {
        ThisItem = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
