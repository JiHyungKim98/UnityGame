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

    public Inventory inventory;
    public GameObject bulletPrefab;
    public Bullet bullet;
    public Bullet bulletObj;
    public Player player;
    public GameObject monsterControllerObj;
    public MonsterController monsterController;

    public GameObject MainWeapon;
    public GameObject CurrentWeapon;
    public GameObject tmpObj;
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

    public void BulletDuration(Bullet bullet)
    {
        _bulletPool.Push(bullet);
    }

    public IEnumerator GiveDamage(Transform monster)
    {
        Debug.Log("GiveDamage");
        monster.GetComponent<Monster>().GetDamageBat();
        yield break;
    }

    public void SetMainWeapon(GameObject obj,GameObject slot)
    {
        if (MainWeapon.transform.childCount == 0) // MainWeapon is null
        {
            obj.GetComponentInParent<Image>().sprite = null; // slot img to null
            obj.transform.SetParent(MainWeapon.transform);
        }
        else // already has
        {
            MainWeapon.transform.GetChild(0).SetParent(tmpObj.transform);
            obj.transform.SetParent(MainWeapon.transform);
            tmpObj.transform.GetChild(0).SetParent(slot.transform);
            slot.transform.GetComponent<Image>().sprite= inventory.SetSprite(slot.transform.GetChild(0).gameObject);
            slot.transform.GetChild(0).gameObject.SetActive(false);
        }

        // Current Weapon sprite change
        CurrentWeapon.GetComponent<Image>().sprite = inventory.SetSprite(MainWeapon.transform.GetChild(0).gameObject);

        
        MainWeapon.transform.GetChild(0).gameObject.SetActive(true);
        MainWeapon.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 0, 0);

        switch (obj.name)
        {
            case "Gun":
                MainWeapon.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Euler(new Vector3(180, -90, -90));
                break;
            case "Paddle":
                MainWeapon.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            default:
                break;
        }
        
    }
}
