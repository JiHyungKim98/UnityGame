using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public TextMeshProUGUI txt;

    public void PopUpUI(string str)
    {
        Debug.Log("PopUpUI1");
        this.gameObject.SetActive(true);
        txt.text = str;
        StartCoroutine(BlinkUI());
        //this.gameObject.SetActive(false);
    }

    IEnumerator BlinkUI()
    {
        Debug.Log("PopUpUI2");
        yield return new WaitForSeconds(2.0f);
        this.gameObject.SetActive(false);
    }
    //{
    //    int count = 0;
    //    while (count < 3)
    //    {
    //        this.gameObject.SetActive(true);
    //        yield return new WaitForSeconds(1f);
    //        this.gameObject.SetActive(false);
    //        yield return new WaitForSeconds(0.5f);
    //        count++;
    //    }
       
        
    
}
