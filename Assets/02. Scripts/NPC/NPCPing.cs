using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPing : NPC
{
    public Sprite img;

    private void OnMouseDown()
    {
        popUp.PopUpUIConversation("<NPC ��>\n���⼭ ���ʹ� �����̴�.���ο��� ������� ��������.�װ͵��� �����߸��⿡�� ���� ��������.������ �ѱ�� ���� ��忡 ��Ƴ���.", img);
    }
}
