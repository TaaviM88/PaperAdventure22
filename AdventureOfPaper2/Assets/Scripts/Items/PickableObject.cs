using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PickableObject : MonoBehaviour
{
    public PickableObjState state = PickableObjState.none;
    Rigidbody2D rb2d;
    BoxCollider2D boxColl;
    bool canBeLifted = true;
    int spawnID;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        if(state == PickableObjState.lifted)
        {
           // transform.localPosition = Vector3.zero;
        }
    }

    public void SetObjectState(PickableObjState newState)
    {
        state = newState;
        DoAction();
    }

    public void DoAction()
    {
        switch (state)
        {
            case PickableObjState.none:
                break;
            case PickableObjState.lifted:
               
                canBeLifted = false;
                break;
            case PickableObjState.lowered:
                rb2d.isKinematic = false;
                boxColl.isTrigger = false;
                canBeLifted = true;

                break;
            default:
                break;
        }
    }

    public void DisableColliderAndSetKinematic()
    {
        rb2d.isKinematic = true;
        boxColl.isTrigger = true;
    }

    public bool CanLift()
    {
        return canBeLifted;
    }
    public void SetSpawnerID(int newID)
    {
        spawnID = newID;
    }

    protected void DestroyGameobj()
    {
        //GameEvents.current.SpawnObject(spawnID);
        Destroy(gameObject);
    }
}
