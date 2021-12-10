using UnityEngine;

namespace ZombieWorld
{
    public class BaseCharacter : MonoBehaviour
    {
        private float hp;

        private void TakeDamage(float damage)
        {
            hp -= damage;
        }
    }
}