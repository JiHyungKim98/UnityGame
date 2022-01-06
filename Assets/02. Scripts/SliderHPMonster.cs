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

    // Start is called before the first frame update
    void Awake()
    {
        sliderHP = GetComponent<Slider>();
        obj = transform.Find("Fill Area").gameObject;
    }
    private void Start()
    {
        monster = GameObject.FindWithTag("Enemy").GetComponent("Monster") as Monster;
    }
    // Update is called once per frame
    void Update()
    {
        sliderHP.value = monster.HP;

        if (sliderHP.value <= 0)
            obj.SetActive(false);
        else
            obj.SetActive(true);
    }
}
