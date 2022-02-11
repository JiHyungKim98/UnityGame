using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using ZombieWorld;

public class WeaponContainer : MonoBehaviour
{
    [SerializeField] public List<GameObject> _bullets = new List<GameObject>();
    private GameObjectPool<Bullet> _bulletPool;

    
    public GameObject bulletPrefab;
    public Bullet bullet;
    public Bullet bulletObj;
    public Player player;
    public GameObject monsterControllerObj;
    public MonsterController monsterController;
    [FormerlySerializedAs("weaponchild")] public Weapon weapon;

    public bool isGun;

    
    private void Start()
    {
        player = GetComponentInParent<Player>();
        monsterController = monsterControllerObj.GetComponent<MonsterController>();
        weapon = GetComponentInChildren<Weapon>();

        bullet = bulletPrefab.GetComponent<Bullet>();
        _bulletPool = new GameObjectPool<Bullet>(20, () =>
        {
            var poolBullet = Instantiate(bullet);
            return poolBullet;
        });

        foreach (Transform child in transform.GetChild(0).transform)
        {
            _bullets.Add(child.gameObject);
        }
    }

    public void Attack()
    {
        for(int i=0;i<monsterController._monsters.Count;i++)
        {
            if (monsterController._monsters[i].activeSelf == true)
            {
                if (Vector3.Distance(this.transform.position, monsterController._monsters[i].transform.position) <= 3.0f)
                {
                    StartCoroutine(GiveDamage(monsterController._monsters[i].transform));
                }
            }

            else
            {
                Debug.Log("Active ����");
            }
            
        }

    }

    public void Fire()
    {
        bulletObj=_bulletPool.Pop();
        bulletObj.gameObject.transform.position =
            new Vector3(
                transform.GetComponentInChildren<Weapon>().transform.position.x
                ,transform.GetComponentInChildren<Weapon>().transform.position.y
                ,transform.GetComponentInChildren<Weapon>().transform.position.z);
            
        Bullet bulletscript = bulletObj.GetComponent<Bullet>();
        
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
}
