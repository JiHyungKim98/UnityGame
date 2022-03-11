using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPing : NPC
{
    public Sprite img;

    private void OnMouseDown()
    {
        popUp.PopUpUIConversation("<NPC 핑>\n여기서 부터는 마을이다.내부에는 좀비들이 가득하지.그것들을 쓰러뜨리기에는 총이 제격이지.여분의 총기는 내가 운동장에 모아놨어.", img);
    }
}
