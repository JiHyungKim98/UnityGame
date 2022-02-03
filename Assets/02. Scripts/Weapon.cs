using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;


public class Weapon : MonoBehaviour
{
    [SerializeField] public List<GameObject> _bullets = new List<GameObject>();
    [SerializeField] public List<GameObject> _weapons = new List<GameObject>();
    private GameObjectPool<Bullet> _bulletPool;
    public int weaponCnt = 0;
    public GameObject bulletPrefab;
    public Bullet bullet;
    public Bullet bulletObj;

    public Player player;
    //public GameObject gun;
    public MonsterController monsterController;
    public WeaponChild weaponchild;

    public bool isGun;
    private void Awake()
    {
        //bulletPrefab = Resources.Load("Bullet") as GameObject;
        player = GameObject.FindWithTag("Player").GetComponent("Player") as Player;
        monsterController = GameObject.FindWithTag("MonsterController").GetComponent("MonsterController") as MonsterController;
        weaponchild = GameObject.FindWithTag("Weapon").GetComponent("WeaponChild") as WeaponChild;
        //gun = GameObject.Find("Gun");
    }
    private void Start()
    {
        

    }
    private void Update()
    {
        
        if (_weapons.Count > 0)
        {
            if (!isGun)
            {
                isGun = true;
                for (int i = 0; i < _weapons.Count; i++)
                {
                    if (_weapons[i].gameObject.name == "gun")
                    {
                        Debug.Log("Weapon gun list");
                        bullet = bulletPrefab.GetComponent("Bullet") as Bullet;


                        _bulletPool = new GameObjectPool<Bullet>(20, () =>
                        {
                            var poolBullet = Instantiate(bullet);
                            //Instantiate(bullet, this.transform.GetChild(0).GetChild(0).transform); // shooter
                            return poolBullet;
                        });
                        //for (int i = 0; i < _bulletPool.Count; i++) // Initialize Bullet
                        //{
                        //    _bulletPool.Pop();
                        //}
                        //foreach (Transform child in transform.GetChild(0).transform)
                        foreach (Transform child in transform.GetChild(0).transform)
                        {
                            _bullets.Add(child.gameObject);
                        }
                    }
                }
            }
            
            
        }
    }

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
        bulletObj.gameObject.transform.position=new Vector3(this.transform.GetChild(0).GetChild(0).position.x
            ,this.transform.GetChild(0).GetChild(0).position.y
            , this.transform.GetChild(0).GetChild(0).position.z);
        Bullet bulletscript = bulletObj.GetComponent("Bullet") as Bullet;
        bulletscript.Shoot();
    }
    public void MonsterAttack(Bullet bullet)
    {
        _bulletPool.Push(bullet);
    }

    public IEnumerator GiveDamage(Transform monster)
    {
        Debug.Log("GiveDamage");
        monster.GetComponent<Monster>().GetDamage();
        yield break;
    }

    public void GetWeapon()
    {
        Debug.Log("GetWeapon()");
        weaponchild.IsNear();
    }
}
