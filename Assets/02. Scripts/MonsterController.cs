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
    [SerializeField] public List<GameObject> _spawnPoints = new List<GameObject>();
    private GameObjectPool<Monster> _monsterPool;
    public GameObject monsterPrefab;
    public Monster monster;
    public Monster monsterObj;
    public Quest quest;
    private int Spawner;
    public bool isDie;

    private void Start()
    {
        //Spawner = 0;
        //monster = monsterPrefab.GetComponent<Monster>();

        //_monsterPool = new GameObjectPool<Monster>(10, ()=> 
        //{
        //    //Debug.Log(UnityEngine.Random.Range(0, 10));
        //    var poolMonster = Instantiate(monster); 
        //    return poolMonster;
        //});
        

        //for (int i = 0; i < _monsterPool.Count; i++)
        //{
        //    _monsterPool.Pop();
        //}
        //foreach(Transform child in transform)
        //{
        //    _monsters.Add(child.gameObject);
        //}
    }
    //public Transform spawnPoints()
    //{

    //    float temp = Time.time * 1000f;
    //    UnityEngine.Random.InitState((int)temp);
    //    int idx = UnityEngine.Random.Range(0, 10);

    //    return _spawnPoints[idx].transform;
    //}
    public void OnDie(Monster obj)
    {
        //Spawner++;
        //_monsterPool.Push(obj);
        //GetComponentInChildren<Monster>().enabled = false;
        //if (!isDie)
        //{
        //    isDie = true;
        //    quest.FirstDieEnemy();
        //}
        StartCoroutine(SpawnTime(obj));
    }
    IEnumerator SpawnTime(Monster obj)
    {
        yield return new WaitForSeconds(10f);
        obj.gameObject.SetActive(true);
    }

    private void Update()
    {
        //if (Spawner >= 1)
        //{
        //    for(int i = 0; i < Spawner; i++)
        //    {
        //        monsterObj=_monsterPool.Pop();
        //        //monsterObj.OnSpawn();
        //    }
        //    Spawner = 0;
        //}
    }
}
