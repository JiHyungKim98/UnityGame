using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;


public class Weapon : MonoBehaviour
{
    [SerializeField] public List<GameObject> _bullets = new List<GameObject>();
    private GameObjectPool<Bullet> _bulletPool;
    public GameObject bulletPrefab;
    public Bullet bullet;
    public Bullet bulletObj;

    public Player player;

    public MonsterController monsterController;

    private void Awake()
    {
        bulletPrefab = Resources.Load("Bullet") as GameObject;

        player = GameObject.FindWithTag("Player").GetComponent("Player") as Player;
        monsterController = GameObject.FindWithTag("MonsterController").GetComponent("MonsterController") as MonsterController;
    }
    private void Start()
    {
        Instantiate(bulletPrefab);
        bullet = bulletPrefab.GetComponent("Bullet") as Bullet;

        _bulletPool = new GameObjectPool<Bullet>(20, () =>
        {
            var poolBullet = Instantiate(bullet, this.transform.GetChild(0).GetChild(0).transform); // shooter
            return poolBullet;
        });
        //for (int i = 0; i < _bulletPool.Count; i++) // Initialize Bullet
        //{
        //    _bulletPool.Pop();
        //}
        foreach (Transform child in transform.GetChild(0).transform)
        {
            _bullets.Add(child.gameObject);
        }

    }

    // Update is called once per frame
    public void Attack()
    {
        for(int i=0;i<monsterController._monsters.Count;i++)
        {
            if (monsterController._monsters[i].activeSelf == true)
            {
                Debug.Log("Active ÄÑÁü");
                if (Vector3.Distance(this.transform.position, monsterController._monsters[i].transform.position) <= 3.0f)
                {
                    StartCoroutine(GiveDamage(monsterController._monsters[i].transform));
                }
            }

            else
            {
                Debug.Log("Active ²¨Áü");
            }
            
        }

    }

    public void Fire()
    {
        bulletObj=_bulletPool.Pop();


    }
    
    public IEnumerator GiveDamage(Transform monster)
    {
        Debug.Log("GiveDamage");
        monster.GetComponent<Monster>().GetDamage();
        yield break;
    }
}
