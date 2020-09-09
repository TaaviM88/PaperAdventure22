using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public DoorController door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void  OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(door.GetIsDoorLocked())
            {
               if(collision.gameObject.GetComponent<PlayerInventory>().GetSmallKeyAmount() > 0)
               {
                   collision.gameObject.GetComponent<PlayerInventory>().RemoveSmallKey();
                   door.SetDoorUnlocked();
                   door.OpenDoor();
               }
            }
        }
    }
}
