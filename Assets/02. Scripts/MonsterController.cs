using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] public List<GameObject> _monsters = new List<GameObject>();
    private GameObjectPool<Monster> _monsterPool;
    public GameObject monsterPrefab;
    public Monster monster;
    public Monster monsterObj;



    private int Spawner;

    private void Awake()
    {
        monsterPrefab = Resources.Load("Zombie_coworker") as GameObject;
    }

    private void Start()
    {
        Spawner = 0;
        //Instantiate(monsterPrefab); // 지우기
        monster = monsterPrefab.GetComponent("Monster") as Monster;

        _monsterPool = new GameObjectPool<Monster>(3, ()=> 
        {
            var poolMonster = Instantiate(monster,this.transform); 
            return poolMonster;
        });

        for (int i = 0; i < _monsterPool.Count; i++) // 처음에 최대 몬스터 SetActive(true)
        {
            //_monsters=
            _monsterPool.Pop();
        }
        //child = new Transform[transform.GetChildCount()];
        foreach(Transform child in transform)
        {
            _monsters.Add(child.gameObject);
        }
        //_monsters.RemoveAt(0);
    }

    public void OnDie(Monster obj)
    {
        Spawner++;
        Debug.Log("Monster Queue에 넣기");
        _monsterPool.Push(obj);
        //obj.OnSpawn();
    }
    private void Update()
    {
        if (Spawner >= 1)
        {
            for(int i = 0; i < Spawner; i++)
            {
                Debug.Log("Monster Queue에서 꺼내기");
                monsterObj=_monsterPool.Pop();
                monsterObj.OnSpawn();
            }
            Spawner = 0;
        }
    }
}
