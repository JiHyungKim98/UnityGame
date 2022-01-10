using System;
using UnityEngine;
using UnityEngine.AI;

using System.Collections;

namespace ZombieWorld
{
    
    [RequireComponent(typeof(NavMeshAgent))]
    public class Monster : BaseCharacter
    {
        /* Enemy Move */
        public float enemyMoveTime;
        public Vector3 direction;
        public float followTime;

        /* Enemy Attack */
        public float attackDelay = 2.0f;
        public bool isAttack=false;
        public bool isFollow;

        /* Enemy HP */
        private float MaxHP = 10f;

        /* Enemy Die */
        public bool isDie = false;

        /* Component Connect */
        protected NavMeshAgent nav;
        GameObject target;
        public Animator animator;
        //public TextMesh txtMeshHP = null;
        private Rigidbody rigidbody;

        /* Script Connect */
        public MonsterObserver observer;
        public Player player;

        Coroutine randPosCoroution = null;
        enum State
        {
            Idle,
            Walk,
            Chase,
            Attack,
            Dead
        }

        State state = State.Idle;

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();

            base.HP = MaxHP;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            
            enemyMoveTime = 2.0f;
            followTime = 5.0f;
        }

        private void Start()
        {
            observer = GameObject.Find("PointOfView").GetComponent("MonsterObserver") as MonsterObserver;
            player = GameObject.Find("Player").GetComponent("Player") as Player;
            target = GameObject.Find("Player");
        }

        void Update()
        {
            if (isDie == true)
            {
                animator.SetFloat("ZombieHP", -1.0f);
                Die();
            }
        }

        private void FixedUpdate()
        {
            /* Enemy Dead */
            if (base.HP < 0.0f || Mathf.Approximately(base.HP, 0.0f))
            {
                base.HP = 0.0f;
                state = State.Dead;
                isDie = true;
            }

            /* Enemy Live */
            else
            {
                animator.SetFloat("ZombieHP", 1.0f);
                /* Enemy Chase & Attack */
                if (observer.m_IsPlayerInRange)
                {
                    /* Attack */
                    if (Vector3.Distance(this.transform.position, player.transform.position) <= 1.5f)
                    {
                        state = State.Attack;
                        if (!isAttack)
                        {
                            isAttack = true;
                            StartCoroutine(Attack());
                        }
                    }
                    /* Chase */
                    else
                    {
                        if (!isFollow) 
                        {
                            isFollow = true;
                            state = State.Chase;
                            StartCoroutine(MoveToTarget());
                        }
                    }

                }
                /* Enemy Walk */
                else
                {
                    state = State.Walk;
                    randPosCoroution = StartCoroutine(randPos());
                }
            }
        }


        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.transform.tag == "Weapon_oneHand" && player.isAttack == true)
        //    {
        //        StartCoroutine(GetDamage());
        //    }
        //}

        public void GetDamage()
        {
            nav.enabled = false;
            base.StartCoroutine(TakeDamage(2));
            this.transform.rotation= Quaternion.LookRotation(player.transform.position-this.transform.position);
            nav.enabled = true;
        }

        public IEnumerator randPos()
        {
            float NewX = UnityEngine.Random.Range(-0.01f, 0.01f);
            float NewZ= UnityEngine.Random.Range(-0.01f, 0.01f);
            Vector3 NewPos = new Vector3(this.transform.position.x+NewX, 0, this.transform.position.z + NewZ).normalized;
            nav.SetDestination(NewPos);
            yield return new WaitForSeconds(enemyMoveTime);
        }

        public IEnumerator MoveToTarget()
        {
            float timer=0;
            while (true)
            {
                timer += Time.deltaTime;
                nav.SetDestination(target.transform.position);
                if (timer >= followTime)
                {
                    isFollow = false;
                    yield break;
                }

                yield return null;
            }
        }
        public void OnSpawn()
        {
            //GameObject SA_Zombie_Bellhop
            
        }

        public void Die()
        {
            StopCoroutine(randPosCoroution);
            StopCoroutine(randPos());
            // ���� pool�� �ִ��۾�
        }
        
        IEnumerator Attack()
        {
            player.GetDamage(10);
            yield return new WaitForSeconds(attackDelay);
            isAttack = false;
        }

        public void UserSkill()
        {
            //TODO: 
        }
    }
}