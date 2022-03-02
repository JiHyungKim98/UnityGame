using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieWorld;

public class SliderHPMonster : MonoBehaviour
{
    Slider sliderHP;
    GameObject obj;
    public Monster monster;
    public Player player;

    // Start is called before the first frame update
    void Awake()
    {
        sliderHP = GetComponent<Slider>();
        obj = transform.Find("Fill Area").gameObject;
    }
    private void Start()
    {
        monster = GetComponentInParent<Monster>();
        //GameObject.FindWithTag("Enemy").GetComponent("Monster") as Monster;
        player = GameObject.FindWithTag("Player").GetComponent("Player") as Player;
    }
    // Update is called once per frame
    void Update()
    {
        sliderHP.value = monster.HP;

        if (sliderHP.value <= 0)
        {
            obj.SetActive(false);
        }
        else
        {
            sliderHP.gameObject.SetActive(true);
            obj.SetActive(true);
        }
    }
}
