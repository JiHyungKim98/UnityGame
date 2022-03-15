using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class MovingUI : MonoBehaviour
{
    public bool isMoving;
    public Image boatIcon;
    public Image Ending;
    public Vector3 StartPos;
    public Vector3 EndPos;
    public Player player;
    public GameObject EndingPos;
    private void Awake()
    {
        StartPos = boatIcon.transform.position;
        EndPos = Ending.transform.position;
    }
    private void Update()
    {
        if (isMoving)
        {
            boatIcon.transform.position = Vector3.MoveTowards(boatIcon.transform.position, EndPos, 8f);
            if (boatIcon.transform.position == EndPos)
            {
                isMoving = false;
                player.MoveToEndingPos();
                this.gameObject.SetActive(false);
                
            }
        }
    }
    public void BoatMove()
    {
        isMoving = true;
        boatIcon.transform.position = StartPos;
    }
}
