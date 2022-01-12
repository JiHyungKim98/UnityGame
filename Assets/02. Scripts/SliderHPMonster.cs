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
        monster = this.transform.parent.parent.parent.gameObject.GetComponent("Monster") as Monster;
        //GameObject.FindWithTag("Enemy").GetComponent("Monster") as Monster;
        player = GameObject.FindWithTag("Player").GetComponent("Player") as Player;
    }
    // Update is called once per frame
    void Update()
    {
        sliderHP.value = monster.HP;

        if (sliderHP.value <= 0)
            sliderHP.gameObject.SetActive(false);
        else
            obj.SetActive(true);
    }
}
