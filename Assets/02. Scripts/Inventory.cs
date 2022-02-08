using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour { 

    [SerializeField] private List<GameObject> _slots = new List<GameObject>();
    [SerializeField] private List<Sprite> _weapon = new List<Sprite>();
    [SerializeField] private List<Sprite> _item = new List<Sprite>();

    public GameObject MainWeaponOjb;
    public GameObject MainWeaponImg;

    
    public void AddToSlot(GameObject obj)
    {
        if (_slots.Count > 16)
        {
            Debug.Log("Slot 개수 초과!");
        }
        else
        {
            foreach(GameObject slot in _slots)
            {
                if (slot.GetComponent<Image>().sprite == null)
                {
                    Debug.Log("여기 비었다!");
                    obj.transform.SetParent(slot.transform);
                    if (obj.name == "Gun") 
                    { 
                    slot.GetComponent<Image>().sprite=_weapon.Find(x => x.name == "gunImg");
                    }
                    else if (obj.name == "Paddle")
                    {
                        slot.GetComponent<Image>().sprite = _weapon.Find(x => x.name == "paddleImg");
                    }
                    else if (obj.name == "MedicBag")
                    {
                        slot.GetComponent<Image>().sprite = _item.Find(x => x.name == "MedicBagImg");
                    }
                    else if (obj.name == "Bandage")
                    {
                        slot.GetComponent<Image>().sprite = _item.Find(x => x.name == "BandageImg");
                    }
                    else if (obj.name == "Candle")
                    {
                        slot.GetComponent<Image>().sprite = _item.Find(x => x.name == "CandleImg");
                    }
                    else if (obj.name == "Map")
                    {
                        slot.GetComponent<Image>().sprite = _item.Find(x => x.name == "MapImg");
                    }
                    else if (obj.name == "Butllet")
                    {
                        slot.GetComponent<Image>().sprite = _item.Find(x => x.name == "BulletImg");
                    }
                    break;
                }
            }

        }

    }
   




}
