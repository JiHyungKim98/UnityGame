using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieWorld
{
<<<<<<< HEAD
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
=======
    public class Player : BaseCharacter
    {


        public float walkSpeed;      // ĳ���Ͱ� ���� �� ���ǵ�.
        public float runSpeed; // ĳ���Ͱ� �� �� ���ǵ�
        public float speed; // ���� ���ǵ�
        public float jumpSpeedF; // ĳ���� ���� ��.
        public float gravity;    // ĳ���Ϳ��� �ۿ��ϴ� �߷�.
        public float fRotSpeed;

        private CharacterController controller; // ���� ĳ���Ͱ� �������ִ� ĳ���� ��Ʈ�ѷ� �ݶ��̴�.
        private Vector3 MoveDir;                // ĳ������ �����̴� ����.

        public Animator animator; // Animator �Ӽ� ���� ����

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

        void Start()
        {
            animator = GetComponent<Animator>(); // animator ������ Player�� Animator �Ӽ����� �ʱ�ȭ

            /*walkSpeed = 0.3f; // walk �ӵ�
            runSpeed = 1f; // run �ӵ�
            fRotSpeed = 100f; // ȸ�� �ӵ�
            jumpSpeedF = 8.0f; // ���� �ӵ�
            gravity = 20.0f; // �߷�*/
>>>>>>> c455e4b94af234772b8f794155e5151a9f0fa674

            MoveDir = Vector3.zero;
            controller = GetComponent<CharacterController>();
        }


<<<<<<< HEAD
    
=======
        
>>>>>>> c455e4b94af234772b8f794155e5151a9f0fa674
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

<<<<<<< HEAD
            if (controller.isGrounded == true) 
=======
            if (controller.isGrounded == true) //ĳ���Ͱ� ���� ��ġ�ϸ�
>>>>>>> c455e4b94af234772b8f794155e5151a9f0fa674
            {
                float fRot = fRotSpeed * Time.deltaTime;


<<<<<<< HEAD
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
                animator.SetInteger("WeaponType_int", 12);
                animator.SetInteger("MeleeType_int", 1);
            }
            else
            {
                animator.SetInteger("WeaponType_int", 0);
                animator.SetInteger("MeleeType_int", 0);
            }

            // attack - two hand
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetInteger("WeaponType_int", 12);
                animator.SetInteger("MeleeType_int", 2);
            }
            else
            {
                animator.SetInteger("WeaponType_int", 0);
                animator.SetInteger("MeleeType_int", 0);
            }
        }

        void OnControllerColliderHit(ControllerColliderHit hit)

        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy");
                TakeDamage(10);
            }
=======
                transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * fRot); //Rotate(ȸ���� ���� ��ǥ �� * ������ * ȸ���ӵ�)

                MoveDir = new Vector3(0, 0, Input.GetAxis("Vertical") * speed); //���ʰ� ������ Ű�����Է��� �̵��� �ƴ� ������ �ٲ�

                //ĳ���ʹ� �����θ� �����̱� ������ Vertical�� Vector���� �����Ѵ�.

                MoveDir = transform.TransformDirection(MoveDir); //���� ��ǥ�� -> ���� ��ǥ��

            }

            // ĳ���Ϳ� �߷� ����.
            MoveDir.y -= gravity * Time.deltaTime;

            //// ĳ���� ������.
            controller.Move(MoveDir * Time.deltaTime);
        }

        private void UpdateState()
        {
            // Move
            if (MoveDir.x > 0 || MoveDir.x < 0)
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
            
>>>>>>> c455e4b94af234772b8f794155e5151a9f0fa674
        }

        private void TakeDamage(float damage)
        {
<<<<<<< HEAD
            //MoveDir.y = jumpSpeedF;
            controller.Move(this.transform.forward * -3.0f);
            CurrentPlayerHP -= damage;
=======
            
>>>>>>> c455e4b94af234772b8f794155e5151a9f0fa674
        }

        private void Heal(float point)
        {
<<<<<<< HEAD
            CurrentPlayerHP += point;
=======
            
>>>>>>> c455e4b94af234772b8f794155e5151a9f0fa674
        }
    }
}
