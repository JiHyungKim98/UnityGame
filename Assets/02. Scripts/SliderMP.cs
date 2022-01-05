using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class SliderMP : MonoBehaviour
{
    Slider sliderMP;
    GameObject FillArea;
    GameObject Fill;
    public Player player;


    private void Awake()
    {
        sliderMP = GetComponent<Slider>();
        FillArea = transform.Find("Fill Area").gameObject;
        //Fill = GameObject.Find("Background");
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent("Player") as Player;
    }
    
    // Update is called once per frame
    void Update()
    {
        sliderMP.value = player.MP;

        if (sliderMP.value <= 0) 
        { 
            FillArea.SetActive(false);
            //Fill.GetComponent<Image>().color=new Color(174/255, 174 / 255, 174 / 255);
        }
        else
        {
            FillArea.SetActive(true);
            //Fill.GetComponent<Image>().color = new Color(255 / 255, 255 / 255, 255 / 255);
        }
            
    }
}
