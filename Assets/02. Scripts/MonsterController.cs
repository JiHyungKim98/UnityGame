using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;

public class MonsterController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Monster> _monsters = new List<Monster>();
    private GameObjectPool<Monster> _monsterPool;
}
