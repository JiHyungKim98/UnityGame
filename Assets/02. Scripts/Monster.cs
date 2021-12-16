using System;
using UnityEngine;
using UnityEngine.AI;

using System.Collections;

namespace ZombieWorld
{
    
    [RequireComponent(typeof(NavMeshAgent))]
    public class Monster : BaseCharacter
    {

        protected NavMeshAgent nav;
        public Vector3 direction;
        public Transform target;
        public float velocity;
        public float enemyMoveTime;
        public float followTime;
        public Vector3 m_direction;


        Rigidbody m_rigidbody;
        public MonsterObserver observer;
        public Player m_player;
        public TextMesh txtEnemyHP = null;

        private float m_MaxHP = 10f;
        private float m_CurrentHP;

        public Animator animator;

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            txtEnemyHP = GameObject.Find("SA_Zombie_Bellhop").GetComponent<TextMesh>();
            base.HP = m_MaxHP;
            observer = GameObject.Find("PointOfView").GetComponent("MonsterObserver") as MonsterObserver;
            m_player = GameObject.Find("Player").GetComponent("Player") as Player;
            m_rigidbody = GetComponent<Rigidbody>();
            m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        void Start()
        {
            m_CurrentHP = m_MaxHP;
            enemyMoveTime = 2.0f;
            followTime = 5.0f;
            
        }
        void Update()
        {
            target = GameObject.Find("Player").transform;
            txtEnemyHP.text = base.HP.ToString();
            if (base.HP<0)
            {
                Die();
            }
            else
            {
                animator.SetFloat("ZombieHP", 1.0f);
            }

            if (observer.m_IsPlayerInRange)
            {
                StartCoroutine(MoveToTarget());
            }
            else
            {
                StartCoroutine(randPos());
            }
            

        }
        public IEnumerator randPos()
        {
            float NewX = UnityEngine.Random.Range(-0.01f, 0.01f);
            float NewZ= UnityEngine.Random.Range(-0.01f, 0.01f);
            Vector3 NewPos = new Vector3(this.transform.position.x+NewX, 0, this.transform.position.z + NewZ).normalized;
            //Debug.Log("���� ���� ��ġ" + NewPos);
            nav.SetDestination(NewPos);
            yield return new WaitForSeconds(enemyMoveTime);
        }

        public IEnumerator MoveToTarget()
        {
            //Debug.Log("after MoveToTarget()");
            this.transform.LookAt(target.transform);
            this.transform.position = Vector3.MoveTowards(this.transform.position,target.transform.position,0.1f);
            yield return new WaitForSeconds(followTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.tag == "Player")
            {
                Attack();
                //Debug.Log("�÷��̾�� �浹!");
            }
            if (other.gameObject.tag == "Weapon_oneHand")
            {
                if (m_player.isSwing == true && m_player.isAttack==true)
                {
                    base.StartCoroutine(TakeDamage(2));
                    this.transform.Translate(m_player.transform.forward*0.5f);
                    this.transform.LookAt(target.transform);
                    //StartCoroutine(MoveToTarget());
                }
                


            }
        }


        public void OnSpawn()
        {
            //GameObject SA_Zombie_Bellhop
            // ���� pool���� ���� �۾�
        }

        public void Die()
        {
            animator.SetFloat("ZombieHP", -1.0f);
            // ���� pool�� �ִ��۾�
        }

        public void Attack()
        {
            m_player.p_TakeDamage(10);
            //m_player.controller.Move(this.transform.forward * 0.5f);
        }

        public void UserSkill()
        {
            //TODO: 
        }
    }
}