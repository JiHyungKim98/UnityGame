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
            Debug.Log("Slot ���� �ʰ�!");
        }
        else
        {
            foreach(GameObject slot in _slots)
            {
                if (slot.transform.childCount==0)
                {
                    obj.transform.SetParent(slot.transform);
                    slot.GetComponent<Image>().sprite = obj.name switch
                    {
                        "Gun" => _weapon.Find(x => x.name == "GunImg"),
                        "Paddle" => _weapon.Find(x => x.name == "PaddleImg"),
                        "MedicBag" => _item.Find(x => x.name == "MedicBagImg"),
                        "Bandage" => _item.Find(x => x.name == "BandageImg"),
                        "Candle" => _item.Find(x => x.name == "CandleImg"),
                        "Map" => _item.Find(x => x.name == "MapImg"),
                        "Bullet" => _item.Find(x => x.name == "BulletImg"),
                        _ => slot.GetComponent<Image>().sprite
                    };
                    break;
                }
            }

        }

    }

    public Sprite SetSprite(GameObject obj)
    {
        switch (obj.name)
        {
            case "Gun":
                Debug.Log("GunImg");
                return _weapon.Find(x => x.name == "GunImg");
            case "Paddle":
                Debug.Log("PaddleImg");
                return _weapon.Find(x => x.name == "PaddleImg");
            case "MedicBag":
                Debug.Log("MedicBagImg");
                return _weapon.Find(x => x.name == "MedicBagImg");
            case "Candle":
                Debug.Log("CandleImg");
                return _weapon.Find(x => x.name == "CandleImg");
            case "Bullet":
                Debug.Log("BulletImg");
                return _weapon.Find(x => x.name == "BulletImg");
            case "Bandage":
                Debug.Log("BandageImg");
                return _weapon.Find(x => x.name == "BandageImg");
            case "Map":
                Debug.Log("BandageImg");
                return _weapon.Find(x => x.name == "MapImg");
            default:
                Debug.Log("elseImg");
                return null;
        }
    }



  
   




}
