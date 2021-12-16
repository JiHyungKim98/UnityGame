using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZombieWorld
{

    public class Player : BaseCharacter
    {
        public float walkSpeed;  
        public float runSpeed;
        public float speed;
        public float jumpSpeedF; 
        public float gravity;  
        public float fRotSpeed;

        public bool isAttack = false;
        public bool isSwing = false;

        private CharacterController controller;
        private Vector3 MoveDir;
        public TextMesh txtPlayerHP=null;

        public Monster p_monster;

        public Animator animator; 
        
        private float p_MaxHP = 100f;
        //private float p_CurrentHP;
        
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
            p_monster= GameObject.Find("SA_Zombie_Bellhop").GetComponent("Monster") as Monster;
            //observer = GameObject.Find("PointOfView").GetComponent("MonsterObserver") as MonsterObserver;
            base.HP = p_MaxHP;

            //walkSpeed = 0.3f;
            //runSpeed = 1f;
            //fRotSpeed = 100f;
            //jumpSpeedF = 8.0f;
            //gravity = 20.0f;
        }

        void Start()
        {
            MoveDir = Vector3.zero;
            controller = GetComponent<CharacterController>();
            txtPlayerHP = GameObject.Find("Player").GetComponent<TextMesh>();
        }

        void Update()
        {
            UpdateState();
            txtPlayerHP.text= base.HP.ToString();
        }

        private void FixedUpdate()
        {
            MoveChracter();
        }


        private void MoveChracter()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
            }
            else
            {
                speed = walkSpeed;
            }

            

            if (controller.isGrounded == true) 

            {
                float fRot = fRotSpeed * Time.deltaTime;

                transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * fRot);

                MoveDir = new Vector3(0, 0, Input.GetAxis("Vertical") * speed); 

               

                MoveDir = transform.TransformDirection(MoveDir); 
            }

           
            MoveDir.y -= gravity * Time.deltaTime;

            controller.Move(MoveDir * Time.deltaTime);
        }

        private void UpdateState()
        {
            // Move
            if (MoveDir.x > 0 || MoveDir.x < 0 || MoveDir.z > 0 || MoveDir.z < 0)
            {
                animator.SetBool("Static_b", false);
                animator.SetFloat("Speed_f", speed);
            }
            else
                animator.SetFloat("Speed_f", 0f);

            // Jump
            if (Input.GetButton("Jump"))
            {
                animator.SetBool("Jump_b", true);
                MoveDir.y = jumpSpeedF;
            }
            else
                animator.SetBool("Jump_b", false);

            // attack - one hand
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("mouse left");
                if (!isAttack) { 
                    StartCoroutine(AttackCoroutine());
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

        protected IEnumerator AttackCoroutine()
        {
            isAttack = true;
            isSwing = true;
            animator.SetInteger("WeaponType_int", 12);
            animator.SetInteger("MeleeType_int", 1);
            yield return new WaitForSeconds(1.0f);
            isSwing = false;
            isAttack = false;
        }

        void OnControllerColliderHit(ControllerColliderHit hit)

        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                controller.Move(this.transform.forward * -3.0f);
                p_TakeDamage(10);

            }
        }
        public void p_TakeDamage(float damage)
        {
            controller.Move(p_monster.transform.forward * 1.0f);
            base.StartCoroutine(TakeDamage(10));
        }
        private void Heal(float point)
        {

            base.HP += point;

        }
    }
}
