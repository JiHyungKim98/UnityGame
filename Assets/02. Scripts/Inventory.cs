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
                if (slot.transform.childCount==0)
                {
                    obj.transform.SetParent(slot.transform);
                    if (obj.name == "Gun") 
                    { 
                        slot.GetComponent<Image>().sprite=_weapon.Find(x => x.name == "GunImg");
                    }
                    else if (obj.name == "Paddle")
                    {
                        slot.GetComponent<Image>().sprite = _weapon.Find(x => x.name == "PaddleImg");
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
                    else if (obj.name == "Bullet")
                    {
                        slot.GetComponent<Image>().sprite = _item.Find(x => x.name == "BulletImg");
                    }
                    break;
                }
            }

        }

    }

    public Sprite SetSprite(GameObject obj)
    {
        if (obj.name == "Gun")
        {
            Debug.Log("GunImg");
            return _weapon.Find(x => x.name == "GunImg");
        }
        else if (obj.name == "Paddle")
        {
            Debug.Log("PaddleImg");
            return _weapon.Find(x => x.name == "PaddleImg");
        }
        else if(obj.name=="MedicBag")
        {
            Debug.Log("MedicBagImg");
            return _weapon.Find(x => x.name == "MedicBagImg");
        }
        else if(obj.name=="Candle")
        {
            Debug.Log("CandleImg");
            return _weapon.Find(x => x.name == "CandleImg");
        }
        else if(obj.name=="Bullet")
        {
            Debug.Log("BulletImg");
            return _weapon.Find(x => x.name == "BulletImg");
        }
        else if(obj.name=="Bandage")
        {
            Debug.Log("BandageImg");
            return _weapon.Find(x => x.name == "BandageImg");
        }
        else if(obj.name=="Map")
        {
            Debug.Log("BandageImg");
            return _weapon.Find(x => x.name == "MapImg");
        }
        else
        {
            Debug.Log("elseImg");
            return null;
        }
    }



  
   




}
