using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZombieWorld
{

    public class Player : BaseCharacter
    {
        /* Player Move */
        public float walkSpeed;  
        public float runSpeed;
        public float currentSpeed;
        public float jumpSpeed; 
        public float gravity;  
        public float rotationSpeed;
        private Vector3 MoveDir;

        /* Player Attack */
        public bool isAttack = false;
        public float attackDelay = 1f;
        public bool isSwing = false;
        GameObject gun;
        GameObject bat;

        /* Player Stat */
        private float MaxHP = 100f;
        private float MaxMP = 5f;
        private float currentMp;
        private float TimerMPPlus=0f;
        private float TimerMPMinus=0f;
        public bool isMPEmpty;
        
        public float MP
        {
            get
            {
                return this.currentMp;
            }
            set
            {
                this.currentMp = value;
            }
        }

        /* Player Die */
        public bool isDie;

        /* Component Connect */
        private CharacterController controller;
        public Animator animator;

        /* Script Connect */
        private Weapon weaponController;
        private Item item;

        enum PlayerAni
        {
            idle=0,
            crossedArm=1,
            HandsOnHips=2,
            CheckWatch=3,
            SexyDance=4,
            Smoking=5,
            Salute=6,
            WipeMount=7,
            LeaningAgainstWall=8,
            SittingOnGround=9
        }

        enum State
        {
            Idle,
            Walk,
            Run,
            Attack,
            Jump,
            Dead
        }
        State state = State.Idle;

        void Awake()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();

            
            base.HP = MaxHP;
            MP = MaxMP;
            MoveDir = Vector3.zero;


            walkSpeed = 0.1f;
            runSpeed = 0.2f;
            rotationSpeed = 2f;
            jumpSpeed = 8.0f;
            gravity = 20.0f;
        }

        void Start()
        {
            weaponController = GetComponentInChildren<Weapon>();
            gun = GameObject.Find("Gun");
            bat = GameObject.Find("Paddle");
            //item = GameObject.FindWithTag("ItemHeal").GetComponent("Bandage") as Bandage;
        }

        void Update()
        {
            //Debug.Log("Player State:" + state);
            UpdateState();            
        }

        private void FixedUpdate()
        {
            MoveChracter();
        }

        private void MoveChracter()
        {
            if (!isDie)
            {
                if (controller.isGrounded == true)
                {
                    transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed);

                    MoveDir = new Vector3(0, 0, Input.GetAxis("Vertical") * currentSpeed);
                    MoveDir = transform.TransformDirection(MoveDir);
                }

                MoveDir.y -= gravity;
                controller.Move(MoveDir);
            }

        }

        private void UpdateState()
        {
            if (base.HP <= 0)
            {
                state = State.Dead;
                isDie = true;
                Die();
            }
            if (!isDie)
            {
                if (MP <= 0)
                {
                    isMPEmpty = true;
                    MP = 0f;
                }
                else if (MP >= 5.0f)
                {
                    MP = 5.0f;
                }

                if (isMPEmpty)
                {
                    StartCoroutine(AllMoveStop());
                }

                /* Run speed */
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    state = State.Run;
                    TimerMPMinus += Time.deltaTime;
                    TimerMPPlus = 0f;

                    if (isMPEmpty == false)
                        currentSpeed = runSpeed;
                    else
                        currentSpeed = 0;

                    if (TimerMPMinus >= 0.02f)
                    {
                        TimerMPMinus = 0f;
                        MP -= 0.1f;
                    }
                }
                /* Walk speed */
                else
                {
                    state = State.Walk;
                    TimerMPPlus += Time.deltaTime;
                    TimerMPMinus = 0f;

                    if (isMPEmpty == false)
                        currentSpeed = walkSpeed;
                    else
                        currentSpeed = 0;

                    
                    if (TimerMPPlus >= 0.05f) 
                    {
                        TimerMPPlus = 0f;
                        MP += 0.1f;
                    }

                    if (MP >= 5f)
                        MP = 5f;

                }


                // Move
                if (MoveDir.x > 0 || MoveDir.x < 0 || MoveDir.z > 0 || MoveDir.z < 0)
                {
                    animator.SetBool("Static_b", false);
                    animator.SetFloat("Speed_f", currentSpeed);
                }
                else
                    animator.SetFloat("Speed_f", 0f);

                // Jump
                if (Input.GetButton("Jump"))
                {
                    state = State.Jump;
                    animator.SetBool("Jump_b", true);
                    MoveDir.y = jumpSpeed;
                }
                else
                    animator.SetBool("Jump_b", false);

                // attack - one hand
                if (Input.GetMouseButtonDown(0))
                {
                    isSwing = true;
                    if (!isAttack)
                    {
                        isAttack = true;
                        if (weaponController.gameObject.transform.GetChild(0).GetChild(0).gameObject == gun)
                        {
                            Debug.Log("Gun Attack success");
                            StartCoroutine(AttackCoroutineGun());
                        }
                        
                        else if(weaponController.gameObject.transform.GetChild(0).GetChild(0).gameObject == bat)
                        {
                            Debug.Log("Bat Attack success");
                            StartCoroutine(AttackCoroutineBat());
                        }
                        else
                        {
                            Debug.Log("Bat&Gun Attack fail");
                        }
                    }
                    else
                    {
                        Debug.Log("Attack fail");
                        
                    }

                }

                // attack - two hand
                //else if (Input.GetMouseButtonDown(1))
                //{
                //    //Debug.Log("mouse right");
                //    animator.SetInteger("WeaponType_int", 12);
                //    animator.SetInteger("MeleeType_int", 2);
                //}
                //else
                //{
                //    animator.SetInteger("WeaponType_int", 0);
                //    animator.SetInteger("MeleeType_int", 0);
                //}

                /* Item Pick Up */
                if (Input.GetKeyDown(KeyCode.C))
                {
                    item.IsNear();
                    //weaponController.GetWeapon();
                }
                if (Input.GetKeyDown(KeyCode.N))
                {
                    weaponController.GetWeapon();
                }

                /* Weapon Change */
                if (Input.GetKeyDown(KeyCode.H))
                {
                    
                }
            }
            
        }

        //IEnumerator MPFill()
        //{
        //    yield return new WaitForSeconds(3.0f);
        //    MP += 0.1f;
        //    isMPFilling = false;

        //}
        IEnumerator AllMoveStop()
        {
            //Debug.Log("AllMoveStop!");
            //currentSpeed = 0;
            yield return new WaitForSeconds(3.0f);
            isMPEmpty = false;
        }
        protected IEnumerator AttackCoroutineBat()
        {
            state = State.Attack;
            animator.SetInteger("WeaponType_int", 12);
            animator.SetInteger("MeleeType_int", 1);
            yield return new WaitForSeconds(attackDelay * 0.5f);
            weaponController.Attack();
            animator.SetInteger("WeaponType_int", 0);
            animator.SetInteger("MeleeType_int", 0);
            yield return new WaitForSeconds(attackDelay * 0.5f);
            isAttack = false;
            isSwing = false;
            
        }

        protected IEnumerator AttackCoroutineGun()
        {
            state = State.Attack;
            animator.SetBool("Shoot_b", true); 
            animator.SetInteger("WeaponType_int", 2);
            yield return new WaitForSeconds(attackDelay * 1f);
            weaponController.Fire();
            yield return new WaitForSeconds(attackDelay * 1f);
            isAttack = false;
            isSwing = false;
            animator.SetBool("Shoot_b", false);
            animator.SetInteger("WeaponType_int", 0);

        }

        void OnControllerColliderHit(ControllerColliderHit hit)

        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                controller.Move(this.transform.forward * -3.0f);
                GetDamage(10);
            }
        }

        public void GetDamage(float damage)
        {
            base.StartCoroutine(TakeDamage(10));
        }
        private void Heal(float point)
        {
            base.HP += point;

        }
        public void Die()
        {
            animator.SetBool("Death_b", true);
        }
    }
}
