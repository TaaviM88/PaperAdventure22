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
                rb2d.isKinematic = true;
                boxColl.isTrigger = false;
                canBeLifted = false;
                break;
            case PickableObjState.lowered:
                rb2d.isKinematic = false;
                boxColl.isTrigger = true;
                canBeLifted = true;

                break;
            default:
                break;
        }
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
