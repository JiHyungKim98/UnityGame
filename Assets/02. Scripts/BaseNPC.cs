using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;

public class BaseNPC : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    //private void OnMouseDown()
    //{
    //    TargetFind();
    //}

    public void TargetFind()
    {
        target = player;
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    target = player;
        //}
        //if (target != null)
        //{
        //    if (Vector3.Distance(this.transform.position, target.transform.position) > 2.0f)
        //    {
        //        transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, 1f);
        //    }
        //}
        
        
    }
}
