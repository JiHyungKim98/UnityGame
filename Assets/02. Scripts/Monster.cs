using System;
using UnityEngine;
using UnityEngine.AI;

namespace ZombieWorld
{
    
    [RequireComponent(typeof(NavMeshAgent))]
    public class Monster : BaseCharacter
    {
        private NavMeshAgent agent;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void OnSpawn()
        {
            
        }

        public void Die()
        {
            
        }

        public void Attack()
        {
            
        }

        public void UserSkill()
        {
            //TODO: 
        }
    }
}