using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObserver : MonoBehaviour
{
    public Transform player;
    public bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("player in");
            m_IsPlayerInRange = true;
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
