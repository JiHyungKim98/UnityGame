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
        public float attackDelay = 1.0f;
        public bool isSwing = false;

        /* Player Stat */
        private float MaxHP = 100f;
        private float MaxMP = 5f;
        private float currentMp;
        private float TimerMP;

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
        //public TextMesh txtMeshHP=null;
        public Animator animator;

        /* Script Connect */
        public Monster monster;

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
        

        void Awake()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();

            monster = GameObject.FindWithTag("Enemy").GetComponent("Monster") as Monster;
            //txtMeshHP = GameObject.Find("Player").GetComponent<TextMesh>();

            base.HP = MaxHP;
            MP = MaxMP;
            MoveDir = Vector3.zero;
            //isSwing = true;

            walkSpeed = 0.3f;
            runSpeed = 1f;
            rotationSpeed = 10f;
            jumpSpeed = 8.0f;
            gravity = 20.0f;
        }

        void Update()
        {
            UpdateState();
            //txtMeshHP.text= base.HP.ToString();
            
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
                    //float fRot = rotationSpeed;
                    //float fRot = fRotSpeed * Time.deltaTime;

                    transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed);

                    MoveDir = new Vector3(0, 0, Input.GetAxis("Vertical") * currentSpeed);
                    MoveDir = transform.TransformDirection(MoveDir);
                }


                MoveDir.y -= gravity;
                //MoveDir.y -= gravity * Time.deltaTime;

                controller.Move(MoveDir);
                //controller.Move(MoveDir * Time.deltaTime);
            }

        }

        private void UpdateState()
        {
            if (base.HP <= 0)
            {
                isDie = true;
                Die();
            }
            if (!isDie)
            {
                if (MP <= 0)
                {
                    MP = 0f;
                    StartCoroutine(AllMoveStop());
                }
                /* Run speed */
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    TimerMP += Time.deltaTime;
                    if (TimerMP >= 0.01f)
                    {
                        TimerMP = 0f;
                        MP -= 0.1f;
                    }
                    currentSpeed = runSpeed;
                }
                /* Walk speed */
                else
                {
                    TimerMP = 0f;
                    if (MP > MaxMP)
                        MP = 5.0f;
                    else if (MP <= MaxMP)
                        StartCoroutine(MPFill());
                        //MP += 0.1f;
                    currentSpeed = walkSpeed;
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

                        Debug.Log("Attack success");

                        StartCoroutine(AttackCoroutine());
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
                else
                {
                    animator.SetInteger("WeaponType_int", 0);
                    animator.SetInteger("MeleeType_int", 0);
                }
            }
            
        }
        IEnumerator MPFill()
        {
            MP += 0.1f;
            yield return new WaitForSeconds(1.0f);
        }
        IEnumerator AllMoveStop()
        {
            Debug.Log("AllMoveStop!");
            currentSpeed = 0;
            yield return new WaitForSeconds(2.0f);
        }
        protected IEnumerator AttackCoroutine()
        {
            
            animator.SetInteger("WeaponType_int", 12);
            animator.SetInteger("MeleeType_int", 1);
            yield return new WaitForSeconds(attackDelay);
            isAttack = false;
            isSwing = false;


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
