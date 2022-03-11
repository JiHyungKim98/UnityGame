using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ZombieWorld;

public class JoyStick : MonoBehaviour
{
    //public bool PlayerMoveFlag;
    ////public bool playerMoveFlag
    ////{
    ////    get
    ////    {
    ////        return this.PlayerMoveFlag;
    ////    }
    ////    set
    ////    {
    ////        this.PlayerMoveFlag = value;
    ////    }
    ////}
    //public CharacterController characterController;
    //public GameObject player;
    //public Animator playerAnimator;
    //Vector3 gravity;
    //public float playerMoveSpeed;
    //public float playerRotSpeed;
    //enum BtnName
    //{
    //    Idle,
    //    Front,
    //    Back,
    //    Left,
    //    Right
    //}
    //BtnName btn = BtnName.Idle;

    //private void Start()
    //{
    //    gravity = player.transform.forward;
    //}
    //private void Update()
    //{
    //    gravity.y -= 9.8f;
    //    if (PlayerMoveFlag)
    //    {
    //        switch (btn)
    //        {
    //            case BtnName.Front:
    //                playerAnimator.SetBool("Static_b", false);
    //                playerAnimator.SetFloat("Speed_f", 0.1f);
    //                characterController.Move(player.transform.forward * playerMoveSpeed * Time.deltaTime);
    //                break;
    //            case BtnName.Back:
    //                characterController.Move(player.transform.forward * -playerMoveSpeed * Time.deltaTime);
    //                break;
    //            case BtnName.Left:
    //                player.transform.Rotate(Vector3.down * playerRotSpeed * Time.deltaTime);
    //                break;
    //            case BtnName.Right:
    //                player.transform.Rotate(Vector3.up * playerRotSpeed * Time.deltaTime);
    //                break;

    //        }
    //    }
    //    else
    //    {
    //        playerAnimator.SetFloat("Speed_f", 0);
    //    }
    //}
    //public void FrontBtnDown()
    //{
    //    PlayerMoveFlag = true;
    //    btn = BtnName.Front;
    //}
    //public void FrontBtnUp()
    //{
    //    PlayerMoveFlag = false;
    //    btn = BtnName.Idle;
    //}
    //public void BackBtnDown()
    //{
    //    PlayerMoveFlag = true;
    //    btn = BtnName.Back;
    //}
    //public void BackBtnUp()
    //{
    //    PlayerMoveFlag = false;
    //    btn = BtnName.Idle;
    //}
    //public void RightBtnDown()
    //{
    //    PlayerMoveFlag = true;
    //    btn = BtnName.Right;
    //}
    //public void RightBtnUp()
    //{
    //    PlayerMoveFlag = false;
    //    btn = BtnName.Idle;
    //}
    //public void LeftBtnDown()
    //{
    //    PlayerMoveFlag = true;
    //    btn = BtnName.Left;
    //}
    //public void LeftBtnUp()
    //{
    //    PlayerMoveFlag = false;
    //    btn = BtnName.Idle;
    //}
    //public Transform player; // player pos
    public Player player;
    public Animator playerAnimator;
    public float currentSpeed = 0f;
    public Transform Stick; // JoyStick

    private Vector3 StickFirstPos;
    private Vector3 JoyVec;
    private float Radius; // JoyStick Background radius
    private bool playerMoveFlag; // Player move switch
    public bool PlayerMoveFlag
    {
        get
        {
            return this.playerMoveFlag;
        }
    }
    private void Start()
    {
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f; // RectTransform y size.
        StickFirstPos = Stick.transform.position; // first stick pos
                                                  // fit radius to canvas size
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;

        playerMoveFlag = false;
    }
    private void FixedUpdate()
    {
        if (playerMoveFlag)
        {

            Vector3 dir = player.gameObject.transform.forward * 0.05f;
            dir.y -= 9.8f;
            player.gameObject.GetComponent<CharacterController>().Move(dir);
            player.gameObject.transform.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
            //Vector3 direction = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
            //player.transform.rotation = Quaternion.Slerp(player.transform.rotation,
            //Quaternion.LookRotation(direction), 0.1f);
            //player.transform.Translate(Vector3.forward * Time.deltaTime * 100f);
            playerAnimator.SetBool("Static_b", false);
            playerAnimator.SetFloat("Speed_f", currentSpeed);
            //player.audioSource.Play();
        }
        else
        {
            playerAnimator.SetFloat("Speed_f", 0);
            //player.audioSource.Stop();
        }
    }

    public void Drag(BaseEventData _Data)
    {
        playerMoveFlag = true;
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        JoyVec = (StickFirstPos - Pos).normalized;

        // My Current Pos - first JoyStick Pos
        float Distance = Vector3.Distance(Pos, StickFirstPos);


        if (Distance < Radius)
        {
            Stick.position = StickFirstPos + (-JoyVec) * Distance;
            currentSpeed = 0.1f;
        }
        else
        {
            Stick.position = StickFirstPos + (-JoyVec) * Radius;
            currentSpeed = 0.2f;
        }

        //player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);


    }


    public void DragEnd()
    {
        Stick.position = StickFirstPos; // move to first pos
        JoyVec = Vector3.zero; // vector to zero
        playerMoveFlag = false;
    }
}



