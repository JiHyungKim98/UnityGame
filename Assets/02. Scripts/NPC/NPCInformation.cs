using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInformation : NPC
{
    public Sprite imgHoon;
    public Sprite imgPing;

    
    private void OnMouseDown()
    {
        switch (gameObject.name)
        {
            case "NPCHoon":
                Hoon();
                break;
            case "NPCPing":
                Ping();
                break;
            default:
                Debug.Log("err");
                break;

        }
    }
    private void Hoon()
    {
        popUp.PopUpUIConversation("<NPC ��>\n�� ã�ƿԱ�.���� ���̺� ������ ��������. ������ ���� �Ա��� ����� �ڶ��.���� �ڻ�� ��� �濪���� �԰��־�.�׸� ã�ư�����.", imgHoon);

    }
    private void Ping()
    {
        popUp.PopUpUIConversation("<NPC ��>\n���⼭ ���ʹ� �����̴�.���ο��� ������� ��������.�װ͵��� �����߸��⿡�� ���� ��������.������ �ѱ�� ���� ��忡 ��Ƴ���.", imgPing);

    }
}
