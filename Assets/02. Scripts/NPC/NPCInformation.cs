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
        popUp.PopUpUIConversation("<NPC 훈>\n잘 찾아왔군.여기 테이블에 지도를 가져가게. 마을로 들어가는 입구는 비행기 뒤라네.나노 박사는 흰색 방역복을 입고있어.그를 찾아가보게.", imgHoon);

    }
    private void Ping()
    {
        popUp.PopUpUIConversation("<NPC 핑>\n여기서 부터는 마을이다.내부에는 좀비들이 가득하지.그것들을 쓰러뜨리기에는 총이 제격이지.여분의 총기는 내가 운동장에 모아놨어.", imgPing);

    }
}
