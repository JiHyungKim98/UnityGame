using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private List<Monster> _monsters = new List<Monster>();
    private GameObjectPool<Monster> _monsterPool;
    public Monster c_monster;
    private void Start()
    {
        c_monster = GameObject.Find("SA_Zombie_Bellhop").GetComponent("Monster") as Monster;
    }

    void RandomPos()
    {

    }

}
