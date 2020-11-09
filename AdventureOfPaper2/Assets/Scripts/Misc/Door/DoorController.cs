using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DoorController : MonoBehaviour
{
    DoorState state = DoorState.Close;
    public int ID = 0;
    public Transform movePoint;
    public float speed = 1f;
    public bool stayOpen = true;
    public bool isLockedByButton = false;
    public bool isLockedByKey = false;
    private Vector3 startpoint;
    private bool isMoving { get; set; }
    bool islockedbyButtonOrig;
    // Start is called before the first frame update
    void Start()
    {
        startpoint = transform.localPosition;
        islockedbyButtonOrig = isLockedByButton; 
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            switch (state)
            {
                case DoorState.Close:

                    transform.DOLocalMoveY(startpoint.y, speed).OnComplete(() => LockAgainDoor());

                    //if (transform.position.y != startpoint.y)
                    //{   
                    //    transform.position = Vector3.Lerp(transform.position, startpoint, speed * Time.deltaTime);
                    //}
                    //else
                    //{
                    //    LockAgainDoor();

                    //}
                    break;

                case DoorState.Open:
                    //if(transform.position.y != movePoint.position.y)
                    //{
                    //    transform.position = Vector3.Lerp(transform.position, movePoint.position,speed * Time.deltaTime);
                    //}
                    transform.DOLocalMoveY(movePoint.localPosition.y, speed);
                break;

                case DoorState.Locked:

                break;
            }
        }
    }

    private void LockAgainDoor()
    {
        Debug.Log("lul");
        if (islockedbyButtonOrig)
        {
            isLockedByButton = islockedbyButtonOrig;
        }
        isMoving = false;
        state = DoorState.Close;
    }

    public void OpenDoor()
    {
        if(isLockedByKey)
        {
            return;
        }


        state = DoorState.Open;
        isMoving = true;
    }

    public void CloseDoor()
    {
        if(islockedbyButtonOrig)
        {
            state = DoorState.Close;
            isMoving = true;
        }
      
    }

    public bool GetIsDoorLocked()
    {
        return isLockedByKey;
    }
    
    public void SetDoorUnlocked()
    {
        isLockedByKey = false;
    
        isLockedByButton = false;
    }

}
