using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class SliderHP : MonoBehaviour
{
    Slider sliderHP;

    GameObject obj;
    public Player player;

    // Start is called before the first frame update
    void Awake()
    {
        sliderHP = GetComponent<Slider>();
        obj = transform.Find("Fill Area").gameObject;
        
    }
    void Start()
    {
        player = GameObject.Find("Player").GetComponent("Player") as Player;
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderHP.value = player.HP;

        if (sliderHP.value <= 0)
            obj.SetActive(false);
        else
            obj.SetActive(true);
    }
}
