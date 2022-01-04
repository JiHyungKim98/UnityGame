using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class SliderMP : MonoBehaviour
{
    Slider sliderMP;
    GameObject obj;
    public Player player;


    private void Awake()
    {
        sliderMP = GetComponent<Slider>();
        obj = transform.Find("Fill Area").gameObject;
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
            obj.SetActive(false);
        else
            obj.SetActive(true);
    }
}
