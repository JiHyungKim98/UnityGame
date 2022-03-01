using System;
using UnityEngine;
using UnityEngine.AI;

using System.Collections;
using UnityEngine.Serialization;

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
        public bool hasTarget;
        public Animator animator;
        //private new Rigidbody rigidbody;

        /* Script Connect */
        public MonsterObserver observer;
        public MonsterController monsterController;
        public Player playerScript;
        public GameObject playerObj;
        [FormerlySerializedAs("weapon")] public WeaponContainer weaponContainer;

        Coroutine randPosCoroution = null;

        public Transform firstPos;
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

            base.HP = MaxHP;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            
            enemyMoveTime = 2.0f;
            followTime = 5.0f;
            firstPos = this.transform;
        }

        private void Start()
        {
            observer = GetComponentInChildren<MonsterObserver>();
            playerScript=playerObj.GetComponent<Player>();
            monsterController = GetComponentInParent<MonsterController>();
        }

        void Update()
        {
            /* Enemy Die */
            if (base.HP < 0.0f || Mathf.Approximately(base.HP, 0.0f) && isDie)
            {
                isDie = true;
                StartCoroutine(Die());
            }

            /* Enemy Live */
            else
            {
                animator.SetFloat("ZombieHP", 1.0f);
                /* Enemy Chase & Attack */

                //if (observer.m_IsPlayerInRange)
                if(Vector3.Distance(this.transform.position,playerScript.transform.position)<=10f)
                {
                    target = playerObj.gameObject;
                    /* Attack */
                    if (Vector3.Distance(this.transform.position, playerScript.transform.position) <= 1.5f)
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
                        if (!isFollow)
                        {
                            isFollow = true;
                            StartCoroutine(Chase());
                        }
                    }

                }
                /* Enemy Walk */
                else
                {
                    if (!isRandomPosEnd)
                    {
                        isRandomPosEnd = true;
                        randPosCoroution = StartCoroutine(randPos());
                    }

                }
            }
        }

       

       

        public void GetDamageBat()
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

        public IEnumerator Chase()
        {
            while (true)
            {
                if (Vector3.Distance(this.transform.position, target.transform.position) >= 10)
                {
                    isFollow = false;
                    yield break;
                }
                nav.SetDestination(target.transform.position);
                yield return null;
            }
        }
       
        public void OnSpawn()
        {
            base.HP = MaxHP;
            this.gameObject.transform.position = firstPos.position;
            //StartCoroutine(randPos());
        }

        IEnumerator Die()
        {
            base.HP = 0.0f;
            state = State.Dead;
            animator.SetFloat("ZombieHP", -1.0f);
            yield return new WaitForSeconds(2.0f);
            monsterController.OnDie(this);
        }
        
        IEnumerator Attack()
        {
            playerScript.GetDamage(10);
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
                Debug.Log("�Ѿ�!");
                weaponContainer.MonsterAttack(collision.gameObject.GetComponent<Bullet>());
                GetDamageGun();
            }
        }
    }
}