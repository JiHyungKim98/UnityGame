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
        //public bool isSwing = false;


        /* Player Stat */
        private float MaxHP = 100f;
        private float MaxMP = 5f;
        private float currentMp;
        private float TimerMPPlus=0f;
        private float TimerMPMinus=0f;
        public bool isMPEmpty;

        public GameObject MainWeapon;
        public GameObject SubWeapon;
        public GameObject inventory;

        public Button attackBtn;

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
        public CharacterController controller;
        
        //private CharacterController controller;
        private Animator animator;

        /* Script Connect */
        private WeaponContainer _weaponContainerController; 
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

        }

        void Start()
        {
            _weaponContainerController = GetComponentInChildren<WeaponContainer>();

            attackBtn.onClick.AddListener(Attack);
        }

        void Update()
        {
            UpdateState();            
        }

        private void FixedUpdate()
        {
            //MoveChracter();
        }

        //private void MoveChracter()
        //{
        //    if (!isDie)
        //    {
        //        if (controller.isGrounded == true)
        //        {
        //            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed);

        //            MoveDir = new Vector3(0, 0, Input.GetAxis("Vertical") * currentSpeed);
        //            MoveDir = transform.TransformDirection(MoveDir);
        //        }

        //        MoveDir.y -= gravity;
        //        controller.Move(MoveDir);
        //    }

        //}

        private void UpdateState()
        {
            /* HP <= 0 */
            if (base.HP <= 0)
            {
                state = State.Dead;
                isDie = true;
                Die();
            }

            /* HP > 0 */
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
                //if (Input.GetKey(KeyCode.LeftShift))
                //{
                //    state = State.Run;
                //    TimerMPMinus += Time.deltaTime;
                //    TimerMPPlus = 0f;

                //    if (isMPEmpty == false)
                //        currentSpeed = runSpeed;
                //    else
                //        currentSpeed = 0;

                //    if (TimerMPMinus >= 0.02f)
                //    {
                //        TimerMPMinus = 0f;
                //        MP -= 0.1f;
                //    }
                //}
                ///* Walk speed */
                //else
                //{
                //    state = State.Walk;
                //    TimerMPPlus += Time.deltaTime;
                //    TimerMPMinus = 0f;

                //    if (isMPEmpty == false)
                //        currentSpeed = walkSpeed;
                //    else
                //        currentSpeed = 0;

                    
                //    if (TimerMPPlus >= 0.05f) 
                //    {
                //        TimerMPPlus = 0f;
                //        MP += 0.1f;
                //    }

                //    if (MP >= 5f)
                //        MP = 5f;

                //}


                //// Move
                //if (MoveDir.x > 0 || MoveDir.x < 0 || MoveDir.z > 0 || MoveDir.z < 0)
                //{
                //    animator.SetBool("Static_b", false);
                //    animator.SetFloat("Speed_f", currentSpeed);
                //}
                //else
                //    animator.SetFloat("Speed_f", 0f);

                // Jump
                if (Input.GetButton("Jump"))
                {
                    state = State.Jump;
                    animator.SetBool("Jump_b", true);
                    MoveDir.y = jumpSpeed;
                }
                else
                    animator.SetBool("Jump_b", false);


                

                /* Item Pick Up */
                if (Input.GetKeyDown(KeyCode.C))
                {
                    ItemPick();
                }
                if (Input.GetKeyDown(KeyCode.N))
                {
                    WeaponPick();
                }

                if (Input.GetKeyDown(KeyCode.H))
                {
                    WeaponChange();
                }

            }
            
        }

        void ItemPick()
        {
            //item.IsNear();
        }
        void WeaponPick()
        { 
            MapController.Instance.GetNearestWeapon(transform.position);
        }
        void WeaponChange()
        {
            if (_weaponContainerController._weapons.Count <= 0) // weaponLst empty
            {
                Debug.Log("무기가 없음!");
                return;
            }
            else
            {
                MainWeapon.transform.GetChild(0).SetParent(SubWeapon.transform);
                SubWeapon.transform.GetChild(0).SetParent(MainWeapon.transform);
                SubWeapon.gameObject.SetActive(false);
                MainWeapon.gameObject.SetActive(true);
                //inventory.GetComponent<Inventory>().WeaponChangeUI();
            }
           
        }

        void Attack()
        {
            Debug.Log("버튼 클릭잘됨.");
            if (_weaponContainerController._weapons.Count <= 0) // weapon lst empty
            {
                Debug.Log("무기를 안 갖고있음");
                return;
            }
            else
            {
                //isSwing = true;
                if (!isAttack)
                {
                    isAttack = true;
                    
                    if (MainWeapon.transform.GetChild(0).gameObject.name == "Gun")
                    {
                        Debug.Log("Gun Attack success");
                        StartCoroutine(AttackCoroutineGun());
                    }

                    else if (MainWeapon.transform.GetChild(0).gameObject.name == "Paddle")
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
            
        }
        IEnumerator AllMoveStop()
        {
            yield return new WaitForSeconds(3.0f);
            isMPEmpty = false;
        }
        protected IEnumerator AttackCoroutineBat()
        {
            state = State.Attack;
            animator.SetInteger("WeaponType_int", 12);
            animator.SetInteger("MeleeType_int", 1);
            yield return new WaitForSeconds(attackDelay * 0.5f);
            _weaponContainerController.Attack();
            animator.SetInteger("WeaponType_int", 0);
            animator.SetInteger("MeleeType_int", 0);
            yield return new WaitForSeconds(attackDelay * 0.5f);
            isAttack = false;
            //isSwing = false;
            
        }

        protected IEnumerator AttackCoroutineGun()
        {
            state = State.Attack;
            animator.SetBool("Shoot_b", true); 
            animator.SetInteger("WeaponType_int", 2);
            yield return new WaitForSeconds(attackDelay * 1f);
            _weaponContainerController.Fire();
            yield return new WaitForSeconds(attackDelay * 1f);
            isAttack = false;
            //isSwing = false;
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
