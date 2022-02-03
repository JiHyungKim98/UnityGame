using System.Collections.Generic;
using Jjamcat.Util;
using UnityEngine;

namespace ZombieWorld
{
    public class MapController : Singleton<MapController>
    {
        [SerializeField] private List<Weapon> _weapons;

        public Weapon GetNearestWeapon(Vector3 pos)
        {
            return null;
        }
    }
}