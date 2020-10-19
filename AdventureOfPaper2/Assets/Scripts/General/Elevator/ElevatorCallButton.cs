using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCallButton : MonoBehaviour
{
    public Transform movePointObj;
    public ElevatorController eControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            eControl.MoveToPoint(movePointObj);
        }
    }
}
