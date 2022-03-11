using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCMasterData", menuName = "NPC/NPC Data List", order = 6)]
public class NPCMasterData : ScriptableObject
{
    public List<NPCData> _data;
    public NPCData GetNPC(PopUp.NPCs type)
    {
        return _data[(int)type];
    }
}
