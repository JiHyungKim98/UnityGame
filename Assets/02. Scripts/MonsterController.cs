using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private List<Monster> _monsters = new List<Monster>();
    private GameObjectPool<Monster> _monsterPool;
    public Monster monster;
    //Slider sliderHP;
    //GameObject obj;

    public Player player;
    private int Spawner;



    private void Start()
    {
        Spawner = 0;
        monster = GameObject.FindWithTag("Enemy").GetComponent("Monster") as Monster;
        _monsterPool = new GameObjectPool<Monster>(1, ()=> 
        {
            var poolMonster = Instantiate(monster,this.transform); 
            return poolMonster;
        });

        //for (int i = 0; i < _monsterPool.Count; i++) // 처음에 최대 몬스터 SetActive(true)
        //{
        //    _monsterPool.Pop();
        //}
    }

    public void OnDie(Monster obj)
    {
        Spawner++;
        Debug.Log("Monster Queue에 넣기");
        _monsterPool.Push(obj);
    }
    private void Update()
    {
        if (Spawner >= 1)
        {
            for(int i = 0; i < Spawner; i++)
            {
                _monsterPool.Pop();
            }
            Spawner = 0;
        }
    }
}
