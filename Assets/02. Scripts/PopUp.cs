using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public TextMeshProUGUI txtWarning;
    public TextMeshProUGUI txtConversation;
    public GameObject WarningUI;
    public GameObject ConversationUI;
    public GameObject LetterUI;
    public void PopUpUIWarning(string str,float second)
    {
        WarningUI.SetActive(true);
        txtWarning.text = str;
        StartCoroutine(ShowPopUp(second,WarningUI));
    }
    public void PopUpUIConversation(string str,float second)
    {
        ConversationUI.SetActive(true);
        txtConversation.text = str;
        StartCoroutine(ShowPopUp(second,ConversationUI));
    }

    IEnumerator ShowPopUp(float second, GameObject obj)
    {
        yield return new WaitForSeconds(second);
        obj.SetActive(false);
        
    }
    public void Show(string name)
    {
        if (name == "letter")
            LetterUI.SetActive(true);
    }
        
    
}
