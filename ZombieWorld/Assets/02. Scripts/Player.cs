using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieWorld
{
    public class Player : MonoBehaviour{

        public float hp;

        public float walkSpeed; 
        public float runSpeed;
        public float speed;
        public float jumpSpeedF;
        public float gravity; 
        public float fRotSpeed;

        private CharacterController controller;
        private Vector3 MoveDir;            

        public Animator animator;

        private float MaxPlayerHP = 100f;
        private float CurrentPlayerHP;

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
            CurrentPlayerHP = MaxPlayerHP;
            walkSpeed = 0.3f; 
            runSpeed = 1f; 
            fRotSpeed = 100f; 
            jumpSpeedF = 8.0f;
            gravity = 20.0f; 

            MoveDir = Vector3.zero;
            controller = GetComponent<CharacterController>();
        }


    
        void Update()
        {
            UpdateState();

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
            if (MoveDir.x > 0 || MoveDir.x < 0)
            {
                animator.SetBool("Static_b", false);
                animator.SetFloat("Speed_f", speed);
            }
            else
                animator.SetFloat("Speed_f", 0f);
            
            if (Input.GetButton("Jump"))
            {
                animator.SetBool("Jump_b", true);
                MoveDir.y = jumpSpeedF;
            }
            else
                animator.SetBool("Jump_b", false);
        
        }
        void OnControllerColliderHit(ControllerColliderHit hit)

        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy");
                TakeDamage(10);
            }
        }


        private void TakeDamage(float damage)
        {
            //MoveDir.y = jumpSpeedF;
            controller.Move(this.transform.forward * -3.0f);
            CurrentPlayerHP -= 10;
        
        }

        private void Heal(float point)
        {
            CurrentPlayerHP += 10;
        
        }
    }
}
