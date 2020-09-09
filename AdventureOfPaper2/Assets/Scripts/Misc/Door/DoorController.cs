using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    DoorState state = DoorState.Close;
    public int ID = 0;
    public Transform movePoint;
    public float speed = 1f;

    public bool isLocked = false;
    private Vector3 startpoint;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        startpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            switch (state)
            {
                case DoorState.Close:
                    if(transform.position.y != startpoint.y)
                    {
                        transform.position = Vector3.Lerp(transform.position, startpoint, speed * Time.deltaTime);
                    }
                    else
                    {
                        isMoving = false;
                        state = DoorState.Close;
                    }
                break;

                case DoorState.Open:
                    if(transform.position.y != movePoint.position.y)
                    {
                        transform.position = Vector3.Lerp(transform.position, movePoint.position,speed * Time.deltaTime);
                    }
                break;

                case DoorState.Locked:

                break;
            }
        }
    }

    public void OpenDoor()
    {
        if(isLocked)
        {
            return;
        }

        state = DoorState.Open;
        isMoving = true;
    }

    public void CloseDoor()
    {
        state = DoorState.Close;
        isMoving = true;
    }

    public bool GetIsDoorLocked()
    {
        return isLocked;
    }
    
    public void SetDoorUnlocked()
    {
        isLocked = false;        
    }

}
