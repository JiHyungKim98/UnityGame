using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle : MonoBehaviour
{
    public GameObject target;
    public GameObject confirmUI;
    private void Start()
    {
        GetComponent<Toggle>().isOn = false;
    }
    private void Update()
    {
        if (GetComponent<Toggle>().isOn == true)
        {
            target.SetActive(true);
            
        }
        else
        {
            target.SetActive(false);
        }
    }
}
