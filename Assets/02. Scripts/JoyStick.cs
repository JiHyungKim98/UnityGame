using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ZombieWorld;

public class JoyStick : MonoBehaviour
{
    //public Transform player; // player pos
    public GameObject player;
    public Animator playerAnimator;
    public float currentSpeed=0f;
    public float playerSpeed=0f;
    public Transform Stick; // JoyStick

    private Vector3 StickFirstPos;
    private Vector3 JoyVec; 
    private float Radius; // JoyStick Background radius
    private bool playerMoveFlag; // Player move switch

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
            //player.transform.Translate(Vector3.forward * Time.deltaTime * 100f);
            playerAnimator.SetBool("Static_b", false);
            playerAnimator.SetFloat("Speed_f", currentSpeed);
            player.GetComponent<Player>().audioSource.Play();
        }
        else
        {
            playerAnimator.SetFloat("Speed_f", 0);
            player.GetComponent<Player>().audioSource.Stop();
        }
    }

    public void Drag(BaseEventData _Data)
    {
        playerMoveFlag = true;
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        JoyVec = (StickFirstPos-Pos).normalized;

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

        player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);


    }

    public void DragEnd()
    {
        Stick.position = StickFirstPos; // move to first pos
        JoyVec = Vector3.zero; // vector to zero
        playerMoveFlag = false;
    }
}
