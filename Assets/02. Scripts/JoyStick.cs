using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    public Transform Player; // player pos
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
    private void Update()
    {
        if (playerMoveFlag)
        {
            Player.transform.Translate(Vector3.forward * Time.deltaTime * 10f);
        }
    }

    public void Drag(BaseEventData _Data)
    {
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        JoyVec = (Pos - StickFirstPos).normalized;

        // My Current Pos - first JoyStick Pos
        float Distance = Vector3.Distance(Pos, StickFirstPos);

        if (Distance < Radius) // move the distance 
            Stick.position = StickFirstPos + JoyVec * Distance;
        else // move the radius
            Stick.position = StickFirstPos + JoyVec * Radius;

        Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);

    }

    public void DragEnd()
    {
        Stick.position = StickFirstPos; // move to first pos
        JoyVec = Vector3.zero; // vector to zero
        playerMoveFlag = false;
    }
}
