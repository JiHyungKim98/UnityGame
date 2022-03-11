using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCMasterData", menuName = "NPC/NPC Data List", order = 6)]
public class NPCMasterData : ScriptableObject
{
    public List<NPCData> _data;

}

//using System.Collections.Generic;
//using UnityEditorInternal.Profiling.Memory.Experimental;
//using UnityEngine;

//[CreateAssetMenu(fileName = "ItemMasterData", menuName = "Item/Item Data List", order = 5)]
//public class ItemMasterData : ScriptableObject
//{
//    public List<ItemData> _data;

//    public ItemData GetItem(Inventory.Items type)
//    {
//        return _data[(int)type];
//    }
//}