using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PickUp : MonoBehaviour
{
    PlayerEnumManager eManager;
    PlayerAnimationController anime;
    //Movement move;
    [Header("PickUp Gameobjects")]
    public Transform carryNode;
    public Transform interactiveNode;

    [Header("PickUp Parameters")]
    public float interactiveRange = 1f;
    public float pickupSpeed = 1f;
    public LayerMask pickupLayer;

    GameObject carryingObj;
    Vector3 orginalInteractivePosition, orginalCarryingPosition;

    // Start is called before the first frame update
    void Start()
    {
        eManager = GetComponent<PlayerEnumManager>();
        anime = GetComponent<PlayerAnimationController>();
        //move = GetComponent<Movement>();
        orginalInteractivePosition = interactiveNode.localPosition;
        orginalCarryingPosition = carryNode.localPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if( eManager.GetLookDir() == PlayerLookDir.right && interactiveNode.localPosition != orginalInteractivePosition)
        {
            interactiveNode.localPosition = orginalInteractivePosition;
            carryNode.localPosition = orginalCarryingPosition;
        }

        if (eManager.GetLookDir() == PlayerLookDir.left && interactiveNode.localPosition == orginalInteractivePosition)
        {
            interactiveNode.localPosition = new Vector2(interactiveNode.localPosition.x * -1, interactiveNode.localPosition.y);
            carryNode.localPosition = new Vector2(carryNode.localPosition.x * -1, carryNode.localPosition.y);
        }        
    }

    public void PickUpObject()
    {
        switch (eManager.moveState)
        {
            case PlayerMoveState.idle:
                if(Input.GetKeyDown(KeyCode.C))
                {
                    Debug.Log("Nostetaan saatana");
                    if(GetPickableObj() != null)
                    {
                        eManager.SetMoveState(PlayerMoveState.pickingUp);
                    }
                    
                }
                break;
            case PlayerMoveState.walk:
                break;
            case PlayerMoveState.duck:
                break;
            case PlayerMoveState.attack:
                break;
            case PlayerMoveState.jump:
                break;
            case PlayerMoveState.pickingUp:
                Collider2D pickupObj = GetPickableObj();
                if (pickupObj?.GetComponent<PickableObject>() && pickupObj.transform.parent == null && pickupObj.GetComponent<PickableObject>().CanLift())
                {
                    anime.SetTrigger("PickUp");
                    carryingObj = pickupObj.gameObject;
                    carryingObj.GetComponent<PickableObject>().SetObjectState(PickableObjState.lifted);
                    //disabloi pelaajalta jos tarvetta
                    carryingObj.transform.SetParent(carryNode);
                    carryingObj.transform.DOMove(carryNode.position, pickupSpeed).SetEase(Ease.InFlash).OnComplete(() => eManager.SetMoveState(PlayerMoveState.carry));
                }
                else
                {
                    eManager.SetMoveState(PlayerMoveState.idle);
                }
                break;
            case PlayerMoveState.carry:

                break;
            case PlayerMoveState.loweringObj:
                anime.SetTrigger("LowerObj");
                carryingObj?.transform.DOMove(interactiveNode.position, pickupSpeed).SetEase(Ease.InFlash).OnComplete(() => LowerObject());
                break;
            default:
                break;
        }
    }

    private Collider2D GetPickableObj()
    {
        return Physics2D.OverlapCircle(interactiveNode.position, interactiveRange, pickupLayer);
    }

    private void LowerObject()
    {
        carryingObj.GetComponent<PickableObject>().SetObjectState(PickableObjState.lowered);
        carryingObj.transform.parent = null;
        carryingObj = null;
        eManager.SetMoveState(PlayerMoveState.idle);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(interactiveNode.position, interactiveRange);
    }
}
