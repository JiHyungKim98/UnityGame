using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieWorld;

public class Boat : MonoBehaviour
{
    public Player player;
    public void PutOnWater()
    {
        this.transform.SetParent(transform.root.root.root);
        this.gameObject.transform.position = new Vector3(517.1f, -8.70031f, -326.69f);
        this.gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
        player.MoveToBoat();
    }
}
