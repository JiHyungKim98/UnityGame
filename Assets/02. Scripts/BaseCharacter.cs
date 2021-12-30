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
            this.hp -= damage;
            yield return new WaitForSeconds(1.0f);
        }
        private void Update()
        {
            if (hp < 0.0f)
                hp = 0.0f;
        }

    }
}