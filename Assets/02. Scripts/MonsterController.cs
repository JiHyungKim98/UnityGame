using System;
using System.Collections;
using System.Collections.Generic;
using Jjamcat.Util;
using UnityEngine;
using ZombieWorld;
using UnityEngine.UI;

public class MonsterController : Singleton<MonsterController>
{
    [SerializeField] public List<GameObject> _monsters = new List<GameObject>();
    private GameObjectPool<Monster> _monsterPool;
    public GameObject monsterPrefab;
    public Monster monster;
    public Monster monsterObj;

    private int Spawner;


    private void Start()
    {
        Spawner = 0;
        monster = monsterPrefab.GetComponent<Monster>();

        _monsterPool = new GameObjectPool<Monster>(0, ()=> 
        {
            var poolMonster = Instantiate(monster,this.transform); 
            return poolMonster;
        });

        for (int i = 0; i < _monsterPool.Count; i++)
        {
            _monsterPool.Pop();
        }
        foreach(Transform child in transform)
        {
            _monsters.Add(child.gameObject);
        }
    }

    public void OnDie(Monster obj)
    {
        Spawner++;
        _monsterPool.Push(obj);
        GetComponentInChildren<Monster>().enabled = false;
    }

    private void Update()
    {
        if (Spawner >= 1)
        {
            for(int i = 0; i < Spawner; i++)
            {
                monsterObj=_monsterPool.Pop();
                monsterObj.OnSpawn();
            }
            Spawner = 0;
        }
    }
}
