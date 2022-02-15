using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObserver : MonoBehaviour
{
    public bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("player in");
            m_IsPlayerInRange = true;
        }
        else
        {
            //Debug.Log("들어오긴함");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            m_IsPlayerInRange = false;
            Debug.Log("player out");
        }
    }

}
