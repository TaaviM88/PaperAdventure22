using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public int id;

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
        //Jos ovi pitää avata muuten kuin nappia painamalla
        if(!door.isLockedByButton)
        {
            if (collision.tag == "Player")
            {
                if (door.GetIsDoorLocked())
                {
                    if (collision.gameObject.GetComponent<PlayerInventory>().GetSmallKeyAmount() > 0)
                    {
                        collision.gameObject.GetComponent<PlayerInventory>().RemoveSmallKey();
                        door.SetDoorUnlocked();
                        door.OpenDoor();
                    }
                }

            }
        }
        //ovi pitää avata nappia painamalla
        else
        {
            if(collision.tag == "Liftable")
            {
                door.SetDoorUnlocked();
                door.OpenDoor();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            if (collision.tag == "Liftable")
            {
            //door.SetDoorUnlocked();
                door.CloseDoor();
            }
    }
}
