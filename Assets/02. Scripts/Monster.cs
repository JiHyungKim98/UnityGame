using System;
using UnityEngine;
using UnityEngine.AI;
<<<<<<< HEAD
using System.Collections;
=======
>>>>>>> c455e4b94af234772b8f794155e5151a9f0fa674

namespace ZombieWorld
{
    
    [RequireComponent(typeof(NavMeshAgent))]
    public class Monster : BaseCharacter
    {
<<<<<<< HEAD
        protected NavMeshAgent nav;
        public Vector3 direction;
        public Transform target;
        public float velocity;
        public float enemyMoveTime;
        public float followTime;

        public MonsterObserver observer;
        public Player m_player;

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();

        }
        void Start()
        {
            enemyMoveTime = 2.0f;
            followTime = 5.0f;
            observer = GameObject.Find("PointOfView").GetComponent("MonsterObserver") as MonsterObserver;
            m_player = GameObject.Find("Player").GetComponent("Player") as Player;
        }
        void Update()
        {
            target = GameObject.Find("Player").transform;

            if (observer.m_IsPlayerInRange)
            {
                //Debug.Log("before MoveToTarget()");
                StartCoroutine(MoveToTarget());
            }
            else
            {
                StartCoroutine(randPos());
            }

        }
        public IEnumerator randPos()
        {
            float NewX = Random.Range(-0.01f, 0.01f);
            float NewZ= Random.Range(-0.01f, 0.01f);
            Vector3 NewPos = new Vector3(this.transform.position.x+NewX, 0, this.transform.position.z + NewZ).normalized;
            //Debug.Log("현재 랜덤 위치" + NewPos);
            nav.SetDestination(NewPos);
            yield return new WaitForSeconds(enemyMoveTime);
        }

        //public void MoveToTarget()
        //{
        //    Debug.Log("after MoveToTarget()");
        //    this.transform.LookAt(target.transform);
        //    this.transform.position = Vector3.MoveTowards(this.transform.position,target.transform.position,0.1f);
            
        //}
        public IEnumerator MoveToTarget()
        {
            //Debug.Log("after MoveToTarget()");
            this.transform.LookAt(target.transform);
            this.transform.position = Vector3.MoveTowards(this.transform.position,target.transform.position,0.1f);
            yield return new WaitForSeconds(followTime);
        }

        //void OnCollisionEnter(Collision hit)

        //{
        //    if (hit.transform.tag == "Player")
        //    {
        //        Debug.Log("PlayerHit");
        //        //m_player.collider.Move(transform.forward * -3.0f);
        //        MoveToTarget();
        //    }
        //}


=======
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
>>>>>>> c455e4b94af234772b8f794155e5151a9f0fa674
    }
}