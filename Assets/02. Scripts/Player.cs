using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieWorld
{
    public class Player : MonoBehaviour
{

    public float hp;

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

        if (controller.isGrounded == true) //ĳ���Ͱ� ���� ��ġ�ϸ�
        {
            float fRot = fRotSpeed * Time.deltaTime;


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
        
    }

    private void TakeDamage(float damage)
    {
        
    }

    private void Heal(float point)
    {
        
    }
}
}
