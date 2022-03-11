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
    public NPCMasterData masterData;
    public enum NPCs
    {
        NPCHoon,
        NPCPing,
        NPCBoatMan,
        NPCNano
    }

    public void SetConversationUI(GameObject obj) 
    {
        ConversationUI.SetActive(true);
        switch (obj.name)
        {
            case "NPCHoon":
                ImgNpc.sprite = masterData.GetNPC(NPCs.NPCHoon).thumbnail;
                txtConversation.text = masterData.GetNPC(NPCs.NPCHoon).Talk.text;
                break;
            case "NPCPing":
                ImgNpc.sprite = masterData.GetNPC(NPCs.NPCPing).thumbnail;
                txtConversation.text = masterData.GetNPC(NPCs.NPCPing).Talk.text;
                break;
            case "NPCBoatMan":
                ImgNpc.sprite = masterData.GetNPC(NPCs.NPCBoatMan).thumbnail;
                txtConversation.text = masterData.GetNPC(NPCs.NPCBoatMan).Talk.text;
                break;
            case "NPCNano":
                ImgNpc.sprite = masterData.GetNPC(NPCs.NPCNano).thumbnail;
                txtConversation.text = masterData.GetNPC(NPCs.NPCNano).Talk.text;
                break;
            default:
                Debug.Log("err");
                break;

        }
    }
    public void PopUpUIWarning(string str,float second)
    {
        txtWarning.text = str;
        StartCoroutine(ShowPopUp(second,WarningUI));
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
            StartCoroutine(ShowPopUp(1.5f, HurtUI));
        }
            
    }
        
    
}
