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
        public Transform firstPos;
        public float enemyMoveTime;
        public Vector3 direction;
        public float followTime;
        public bool isRandomPosEnd=false;
        public Vector3 default_direction;

        /* Enemy Attack */
        public float attackDelay = 5.0f;
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
        //public MonsterObserver observer;
        public MonsterController monsterController;
        public Player playerScript;
        public GameObject playerObj;
        [FormerlySerializedAs("weapon")] public WeaponContainer weaponContainer;

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

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            nav = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            //base.HP = MaxHP;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            
            enemyMoveTime = 2.0f;
            followTime = 5.0f;
            firstPos = this.transform;

            default_direction.x = UnityEngine.Random.Range(-1.0f, 1.0f);
            default_direction.z = UnityEngine.Random.Range(-1.0f, 1.0f);
        }
        private void OnEnable()
        {
            base.HP = MaxHP;
            //float temp = Time.time * 1000f;
            //UnityEngine.Random.InitState((int)temp);
            animator.SetFloat("ZombieHP", 1.0f);

            //int idx = UnityEngine.Random.Range(0, 10);
            //this.gameObject.transform.position = GetComponentInParent<MonsterController>()._spawnPoints[idx].transform.position;
            this.gameObject.transform.position = firstPos.position;
            StartCoroutine(randPos());
        }

        private void Start()
        {
            //observer = GetComponentInChildren<MonsterObserver>();
            playerScript=playerObj.GetComponent<Player>();
            monsterController = GetComponentInParent<MonsterController>();
        }

        void Update()
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
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
                        StartCoroutine(randPos());
                    }

                }
            }
        }

       

       

        public void GetDamageBat()
        {
            Debug.Log("Bat Damage");
            nav.enabled = false;
            base.StartCoroutine(TakeDamage(3));
            nav.enabled = true;
        }
        public void GetDamageGun()
        {
            Debug.Log("Gun Damage");
            nav.enabled = false;
            base.StartCoroutine(TakeDamage(6));
            nav.enabled = true;
        }
        
       
        public IEnumerator randPos()
        {
            float temp = Time.time * 1000f;
            UnityEngine.Random.InitState((int)temp);

            state = State.Walk;
            float NewX = UnityEngine.Random.Range(-1f, 1f);
            float NewZ= UnityEngine.Random.Range(-1f, 1f);

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
            //base.HP = MaxHP;
            //this.gameObject.transform.position = firstPos.position;
            //StartCoroutine(randPos());
        }

        IEnumerator Die()
        {
            base.HP = 0.0f;
            state = State.Dead;
            animator.SetFloat("ZombieHP", -1.0f);
            yield return new WaitForSeconds(2.0f);
            this.gameObject.SetActive(false);
            //yield return new WaitForSeconds(30f);
            monsterController.OnDie(this);
            
            //monsterController.OnDie(this);
        }
        
        IEnumerator Attack()
        {
            playerScript.GetDamage(10);
            yield return new WaitForSeconds(attackDelay);
            isAttack = false;
        }

        public void UserSkill()
        {
        }



        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Bullet"))
            {
                weaponContainer.MonsterAttack(collision.gameObject.GetComponent<Bullet>());
                
                
                
                GetDamageGun();
            }
            
        }
    }
}