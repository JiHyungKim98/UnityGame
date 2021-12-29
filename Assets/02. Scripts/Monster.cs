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


        public float AtkDelay = 2.0f;
        public bool isAttack;
        Rigidbody rigidbody;
        public MonsterObserver observer;
        public Player player;
        public TextMesh txtEnemyHP = null;

        private float MaxHP = 10f;
        private float CurrentHP;

        public Animator animator;

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            txtEnemyHP = GameObject.Find("SA_Zombie_Bellhop").GetComponent<TextMesh>();
            base.HP = MaxHP;
            observer = GameObject.Find("PointOfView").GetComponent("MonsterObserver") as MonsterObserver;
            player = GameObject.Find("Player").GetComponent("Player") as Player;
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        void Start()
        {
            CurrentHP = MaxHP;
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
        private void FixedUpdate()
        {
            if(Vector3.Distance(this.transform.position,player.transform.position) <= 1.0f)
            {
                if (!isAttack)
                {
                    isAttack = true;
                    StartCoroutine(Attack());
                }
            }
            
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.transform.tag == "Player")
        //    {
        //        Attack();
        //    }
        //    if (other.gameObject.tag == "Weapon_oneHand")
        //    {
        //        if (player.isAttack == true)
        //        {
        //            base.StartCoroutine(TakeDamage(2));
        //            this.transform.Translate(player.transform.forward * 0.5f);
        //            this.transform.LookAt(target.transform);
        //            //StartCoroutine(MoveToTarget());
        //        }
        //    }
        //}
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.transform.tag == "Weapon_oneHand"&&player.isAttack==true)
        //    {
        //        Debug.Log("���⿡ ���ݴ���");
        //    }
        //}

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
        // Attack() �Ѵ붧���� 3�� ��ٸ��� �̷������� �ٲ����
        
        IEnumerator Attack()
        {
            player.GetDamage(10);
            yield return new WaitForSeconds(AtkDelay);
            
            isAttack = false;
        }
        public void GetDamage(float damage)
        {
            base.StartCoroutine(TakeDamage(10));
            this.transform.Translate(player.transform.forward * 0.5f);
            this.transform.LookAt(target.transform);
            StartCoroutine(MoveToTarget());
        }

        //public void Attack() 
        //{
        //    Debug.Log("����?");
        //    player.p_TakeDamage(10);
        //}

        public void UserSkill()
        {
            //TODO: 
        }
    }
}