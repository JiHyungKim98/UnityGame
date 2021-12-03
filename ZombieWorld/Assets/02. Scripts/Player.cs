using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float walkSpeed;      // 캐릭터가 걸을 때 스피드.
    public float runSpeed; // 캐릭터가 뛸 때 스피드
    public float speed; // 현재 스피드
    public float jumpSpeedF; // 캐릭터 점프 힘.
    public float gravity;    // 캐릭터에게 작용하는 중력.
    public float fRotSpeed;

    private CharacterController controller; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    private Vector3 MoveDir;                // 캐릭터의 움직이는 방향.

    public Animator animator; // Animator 속성 변수 생성

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
        animator = GetComponent<Animator>(); // animator 변수를 Player의 Animator 속성으로 초기화

        walkSpeed = 0.3f; // walk 속도
        runSpeed = 1f; // run 속도
        fRotSpeed = 100f; // 회전 속도
        jumpSpeedF = 8.0f; // 점프 속도
        gravity = 20.0f; // 중력

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

        if (controller.isGrounded == true) //캐릭터가 땅에 위치하면
        {
            float fRot = fRotSpeed * Time.deltaTime;


            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * fRot); //Rotate(회전할 기준 좌표 축 * 변위값 * 회전속도)

            MoveDir = new Vector3(0, 0, Input.GetAxis("Vertical") * speed); //왼쪽과 오른쪽 키보드입력은 이동이 아닌 방향을 바꿈

            //캐릭터는 앞으로만 움직이기 때문에 Vertical만 Vector값을 설정한다.

            MoveDir = transform.TransformDirection(MoveDir); //로컬 좌표계 -> 월드 좌표계

        }

        // 캐릭터에 중력 적용.
        MoveDir.y -= gravity * Time.deltaTime;

        //// 캐릭터 움직임.
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

}