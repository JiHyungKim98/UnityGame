using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;


public class Item : MonoBehaviour
{
    public Transform enemy;
    public Player player;
    public Monster monster;

    // Start is called before the first frame update
    private void Awake()
    {
       //isSwing = true;
       player= GameObject.FindWithTag("Player").GetComponent("Player") as Player;
       monster=GameObject.FindWithTag("Enemy").GetComponent("Monster") as Monster;
    }

    // Update is called once per frame
    public void Attack()
    {
        enemy = GameObject.FindWithTag("Enemy").transform;
        if (Vector3.Distance(this.transform.position, enemy.position) <= 3.0f) // && player.isAttack == true
        {
            StartCoroutine(GiveDamage());
        }
    }
    
    public IEnumerator GiveDamage()
    {
        monster.GetDamage();
        yield break;
    }
}
