using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField] private List<GameObject> _quest = new List<GameObject>();
   
    public bool isFirstAtk;
    public bool isFirstUseItem;
    public void FirstAtk()
    {
        _quest[0].gameObject.GetComponent<Toggle>().isOn = true;
    }
    public void FirstUseItem()
    {
        _quest[1].gameObject.GetComponent<Toggle>().isOn = true;
    }
    public void FirstDieEnemy()
    {
        _quest[2].gameObject.GetComponent<Toggle>().isOn = true;
    }

}
