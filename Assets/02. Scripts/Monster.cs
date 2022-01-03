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

        /* Enemy HP */
        private float MaxHP = 10f;

        /* Enemy Die */
        public bool isDie = false;

        /* Component Connect */
        protected NavMeshAgent nav;
        public Transform target;
        public Animator animator;
        public TextMesh txtMeshHP = null;
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

            txtMeshHP = GameObject.Find("SA_Zombie_Bellhop").GetComponent<TextMesh>();
            observer = GameObject.Find("PointOfView").GetComponent("MonsterObserver") as MonsterObserver;
            player = GameObject.Find("Player").GetComponent("Player") as Player;
            

            base.HP = MaxHP;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            

            enemyMoveTime = 2.0f;
            followTime = 5.0f;
        }
        void Update()
        {
            if (isDie == true)
            {
                animator.SetFloat("ZombieHP", -1.0f);
                Die();
            }

            txtMeshHP.text = base.HP.ToString();
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
                        state = State.Chase;
                        StartCoroutine(MoveToTarget());
                    }

                }
                /* Enemy Walk */
                else
                {
                    
                    state = State.Walk;
                    randPosCoroution=StartCoroutine(randPos());
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
            this.transform.Translate(player.transform.forward * 1.0f);
            //this.transform.LookAt(target.transform);
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
            target = GameObject.Find("Player").transform;
            nav.SetDestination(target.position);
            yield return new WaitForSeconds(followTime);

            //this.transform.LookAt(target.transform);
            //this.transform.position = Vector3.MoveTowards(this.transform.position,target.transform.position,0.1f);
        }
        public void OnSpawn()
        {
            //GameObject SA_Zombie_Bellhop
            // 좀비 pool에서 빼는 작업
        }

        public void Die()
        {
            StopCoroutine(randPosCoroution);
            // 좀비 pool에 넣는작업
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