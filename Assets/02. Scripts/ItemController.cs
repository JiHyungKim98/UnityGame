using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;
public class ItemController : IngameController
{
    [SerializeField] public List<GameObject> _item = new List<GameObject>();
    [SerializeField] public List<Sprite> _itemImg = new List<Sprite>();
}
