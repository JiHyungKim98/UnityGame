using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    public TextMeshProUGUI txtWarning;
    public TextMeshProUGUI txtConversation;
    public GameObject WarningUI;
    public GameObject ConversationUI;
    public GameObject LetterUI;
    public GameObject HurtUI;
    public Image ImgNpc;
    public void PopUpUIWarning(string str,float second)
    {
        //WarningUI.SetActive(true);
        txtWarning.text = str;
        StartCoroutine(ShowPopUp(second,WarningUI));
    }
    public void PopUpUIConversation(string str,Sprite Img)
    {
        //ConversationUI.SetActive(true);
        ConversationUI.SetActive(true);
        txtConversation.text = str;
        ImgNpc.sprite = Img;
    }

    IEnumerator ShowPopUp(float second, GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(second);
        obj.SetActive(false);
        
    }
    public void Show(GameObject obj)
    {
        if (obj.name == "letter")
        {
            LetterUI.SetActive(true);
        }
            
        if (obj.name == "Player")
        {
            Debug.Log("player attack");
            //HurtUI.SetActive(true);
            StartCoroutine(ShowPopUp(1.5f, HurtUI));
        }
            
    }
        
    
}
