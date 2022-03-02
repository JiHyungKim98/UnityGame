using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public TextMeshProUGUI txt;

    public void PopUpUI(string str)
    {
        this.gameObject.SetActive(true);
        txt.text = str;
        StartCoroutine(ShowPopUp());
    }

    IEnumerator ShowPopUp()
    {
        yield return new WaitForSeconds(2.0f);
        this.gameObject.SetActive(false);
        
    }
        
    
}
