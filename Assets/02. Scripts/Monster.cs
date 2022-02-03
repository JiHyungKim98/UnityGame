using System;
using UnityEngine;
using UnityEngine.AI;

using System.Collections;

namespace ZombieWorld
{
    
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]

    public class Monster : BaseCharacter
    {
        /* Enemy Move */
        public float enemyMoveTime;
        public Vector3 direction;
        public float followTime;
        public bool isRandomPosEnd=false;

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
        public MonsterController monsterController;
        public Player player;
        public Weapon weapon;

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
            //Debug.Log("monster 스크립트 시작");
            gameObject.GetComponent<Monster>().enabled = true;

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
            // monster = this.transform.parent.parent.parent.gameObject.GetComponent("Monster") as Monster;
            observer = this.transform.Find("PointOfView").GetComponent("MonsterObserver") as MonsterObserver;
            //GameObject.Find("PointOfView").GetComponent("MonsterObserver") as MonsterObserver;
            player = GameObject.Find("Player").GetComponent("Player") as Player;
            target = GameObject.Find("Player");
            monsterController= GameObject.Find("MonsterController").GetComponent("MonsterController") as MonsterController;
            //weapon = GameObject.Find("WeaponController").GetComponent("Weapon") as Weapon;
        }

        void Update()
        {
            //Debug.Log("Monster State:" + state);
            if (isDie)
            {
                isDie = false;
                animator.SetFloat("ZombieHP", -1.0f);
                StartCoroutine(Die());
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
                            //StartCoroutine(MoveToTarget());
                        }
                    }

                }
                /* Enemy Walk */
                else
                {
                    if (!isRandomPosEnd)
                    {
                        isRandomPosEnd = true;
                        //randPosCoroution = StartCoroutine(randPos());
                    }
                    
                }
            }
        }


       

        public void GetDamage()
        {
            Debug.Log("Bat Damage");
            nav.enabled = false;
            base.StartCoroutine(TakeDamage(2));
            nav.enabled = true;
        }
        public void GetDamageGun()
        {
            Debug.Log("Gun Damage");
            nav.enabled = false;
            base.StartCoroutine(TakeDamage(5));
            nav.enabled = true;
        }

        public IEnumerator randPos()
        {
            //Debug.Log("Coroutine: randPos()");
            state = State.Walk;
            float NewX = UnityEngine.Random.Range(-0.01f, 0.01f);
            float NewZ= UnityEngine.Random.Range(-0.01f, 0.01f);
            Vector3 NewPos = new Vector3(this.transform.position.x+NewX, 0, this.transform.position.z + NewZ);
            nav.SetDestination(NewPos);
            yield return new WaitForSeconds(enemyMoveTime);
            isRandomPosEnd = false;
        }

        public IEnumerator MoveToTarget()
        {
            //Debug.Log("Coroutine:MoveToTarget()");

            float timer =0;
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
            base.HP = MaxHP;
            this.gameObject.transform.position=new Vector3(2.31f, -4.87f, 2.735f);
            StartCoroutine(randPos());
        }

        IEnumerator Die()
        {
            StopCoroutine(randPosCoroution);
            StopCoroutine(randPos());
            yield return new WaitForSeconds(2.0f);
            monsterController.OnDie(this);
            //this.gameObject.GetComponent<Monster>().enabled = false;
            //Debug.Log("monster 스크립트 종ㄽ");

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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Bullet"))
            {
                Debug.Log("총알!");
                weapon.MonsterAttack(collision.gameObject.GetComponent<Bullet>());
                GetDamageGun();
            }
        }
    }
}