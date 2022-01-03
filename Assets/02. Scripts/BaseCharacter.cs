using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace ZombieWorld
{
    public class BaseCharacter : MonoBehaviour
    {
        private float hp;
        public float HP
        {
            get
            {
                return this.hp;
            }
            set
            {
                this.hp = value;
            }
        }
        public IEnumerator TakeDamage(float damage)
        {
            if (this.hp < 0)
                this.hp = 0.0f;
            else 
            { 
            this.hp -= damage;
            }
            yield return new WaitForSeconds(1.0f);
        }
        

    }
}