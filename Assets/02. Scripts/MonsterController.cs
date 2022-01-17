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
    Transform[] child;


    private int Spawner;



    private void Start()
    {
        Spawner = 0;
        Instantiate(monsterPrefab);
        monster = monsterPrefab.GetComponent("Monster") as Monster;

        _monsterPool = new GameObjectPool<Monster>(3, ()=> 
        {
            var poolMonster = Instantiate(monster,this.transform); 
            return poolMonster;
        });

        for (int i = 0; i < _monsterPool.Count; i++) // ó���� �ִ� ���� SetActive(true)
        {
            //_monsters=
            _monsterPool.Pop();
        }
        //child = new Transform[transform.GetChildCount()];
        foreach(Transform child in transform)
        {
            _monsters.Add(child.gameObject);
        }
    }

    public void OnDie(Monster obj)
    {
        Spawner++;
        Debug.Log("Monster Queue�� �ֱ�");
        _monsterPool.Push(obj);
    }
    private void Update()
    {
        if (Spawner >= 1)
        {
            for(int i = 0; i < Spawner; i++)
            {
                Debug.Log("Monster Queue���� ������");
                _monsterPool.Pop();
            }
            Spawner = 0;
        }
    }
}
