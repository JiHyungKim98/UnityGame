using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;


public class Weapon : MonoBehaviour
{
    //public Transform enemy;
    public Player player;
    public Transform ParentObj; // 부모 오브젝트
    public Transform[] ChildrenOjb; // 자식 오브젝트 배열
    public MonsterController monsterController;

    private void Awake()
    {
        ParentObj = GameObject.FindWithTag("MonsterController").transform;
        player = GameObject.FindWithTag("Player").GetComponent("Player") as Player;
        monsterController = GameObject.FindWithTag("MonsterController").GetComponent("MonsterController") as MonsterController;

        //foreach (Transform child in transform)
        //{
        //    _monsters.Add(child.gameObject);
        //}

        //Debug.Log("CHild개수"+ParentObj.gameObject.transform.GetChildCount());
        //foreach(Transform child in )
        //ChildrenOjb = new Transform[ParentObj.transform.GetChildCount()];
        //ParentObj.gameObject.GetComponentsInChildren<Transform>();
        //monsterTags = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    public void Attack()
    {
        for(int i=0;i<monsterController._monsters.Count;i++)
        {
            if (monsterController._monsters[i].activeSelf == true)
            {
                Debug.Log("Active 켜짐");
                if (Vector3.Distance(this.transform.position, monsterController._monsters[i].transform.position) <= 3.0f)
                {
                    StartCoroutine(GiveDamage(monsterController._monsters[i].transform));
                }
            }

            else
            {
                Debug.Log("Active 꺼짐");
            }
            
        }

    }
    
    public IEnumerator GiveDamage(Transform monster)
    {
        Debug.Log("GiveDamage");
        monster.GetComponent<Monster>().GetDamage();
        yield break;
    }
}
