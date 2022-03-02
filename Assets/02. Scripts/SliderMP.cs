using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class SliderMP : MonoBehaviour
{
    Slider sliderMP;
    public GameObject FillArea;
    GameObject Fill;
    public Player player;


    private void Awake()
    {
        sliderMP = GetComponent<Slider>();
    }
  
    void Update()
    {
        sliderMP.value = player.MP;

        if (sliderMP.value <= 0) 
        { 
            FillArea.SetActive(false);
        }
        else
        {
            FillArea.SetActive(true);
        }
            
    }
}
