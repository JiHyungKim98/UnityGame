using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public List<GameObject> _weapon = new List<GameObject>();
    [SerializeField] public List<Sprite> _weaponImg = new List<Sprite>();
}
