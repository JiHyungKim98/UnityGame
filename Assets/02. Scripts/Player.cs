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
        public GameObject inventory;
        public GameObject popUp;
        public AudioSource audioSource;
        public AudioClip walkSound;
        public Quest quest;
        public Button attackBtn;
        public bool isFirstAtk;

        public ParticleSystem particleObject;

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
        public WeaponContainer WeaponContainer;

        //private CharacterController controller;
        private Animator animator;

        
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

        public enum State
        {
            Idle,
            Walk,
            Run,
            Attack,
            Jump,
            Dead
        }

        //public State _state 
        //{ get _state; set; }

        State state = State.Idle;

        void Awake()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            base.HP = MaxHP;
            MP = MaxMP;
            MoveDir = Vector3.zero;
            audioSource = this.gameObject.GetComponent<AudioSource>();
            //walkSound = audioSource.clip;
        }

        void Start()
        {
            WeaponContainer = GetComponentInChildren<WeaponContainer>();
            attackBtn.onClick.AddListener(Attack);
        }

        void Update()
        {
            UpdateState();            
        }

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


                

                

            }
            
        }

        
        

        void Attack()
        {
            if (MainWeapon.transform.childCount==0) // weapon lst empty
            {
                popUp.GetComponent<PopUp>().PopUpUIWarning("No weapon.",2f);
                return;
            }
            else
            {
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
                        StartCoroutine(AttackCoroutineBat(MainWeapon.transform.GetChild(0).gameObject));
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
        protected IEnumerator AttackCoroutineBat(GameObject weapon)
        {
            
            state = State.Attack;
            animator.SetInteger("WeaponType_int", 12);
            animator.SetInteger("MeleeType_int", 1);
           
            yield return new WaitForSeconds(attackDelay * 0.3f);
            WeaponContainer.Attack();
            animator.SetInteger("WeaponType_int", 0);
            animator.SetInteger("MeleeType_int", 0);
            yield return new WaitForSeconds(attackDelay * 0.3f);
            isAttack = false;
            

        }

        protected IEnumerator AttackCoroutineGun()
        {
            particleObject.Play();
            state = State.Attack;
            animator.SetBool("Shoot_b", true); 
            animator.SetInteger("WeaponType_int", 2);
            yield return new WaitForSeconds(attackDelay * 0.4f);
            WeaponContainer.Fire();
            yield return new WaitForSeconds(attackDelay * 1f);
            
            
            isAttack = false;
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
        public void Heal(float point)
        {
            Debug.Log("Heal");
            if ((base.HP + point) > 100)
            {
                base.HP = 100;
            }
            else
            {
                base.HP += point;
            }
            

        }
        public void Die()
        {
            animator.SetBool("Death_b", true);
        }

        
    }
}
