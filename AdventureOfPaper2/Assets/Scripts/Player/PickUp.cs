using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PickUp : MonoBehaviour
{
    PlayerEnumManager eManager;
    PlayerAnimationController anime;
    PlayerManager manager;
    //Movement move;
    [Header("PickUp Gameobjects")]
    public Transform carryNode;
    public Transform interactiveNode;

    [Header("PickUp Parameters")]
    public float interactiveRange = 1f;
    public float pickupSpeed = 1f;
    public LayerMask pickupLayer;

    bool canLift = true;

    public GameObject carryingObj { get; private set; }
    Vector3 orginalInteractivePosition, orginalCarryingPosition;

    // Start is called before the first frame update
    void Start()
    {
        eManager = GetComponent<PlayerEnumManager>();
        anime = GetComponent<PlayerAnimationController>();
        manager = GetComponent<PlayerManager>();
        //move = GetComponent<Movement>();
        orginalInteractivePosition = interactiveNode.localPosition;
        orginalCarryingPosition = carryNode.localPosition;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C) && manager.GetCanLift())
        {
            if (carryingObj == null)
            {
                PickUpObject();
            }
            else
            {
                LowerObject();
            }

        }

        if ( eManager.GetLookDir() == PlayerLookDir.right && interactiveNode.localPosition != orginalInteractivePosition)
        {
            interactiveNode.localPosition = orginalInteractivePosition;
            carryNode.localPosition = orginalCarryingPosition;
            if(carryingObj != null)
            {
                carryingObj.transform.localScale = new Vector3(Mathf.Abs(carryingObj.transform.localScale.x), carryingObj.transform.localScale.y, carryingObj.transform.localScale.z);
            }
        }

        if (eManager.GetLookDir() == PlayerLookDir.left && interactiveNode.localPosition == orginalInteractivePosition)
        {
            interactiveNode.localPosition = new Vector2(interactiveNode.localPosition.x * -1, interactiveNode.localPosition.y);
            carryNode.localPosition = new Vector2(carryNode.localPosition.x * -1, carryNode.localPosition.y);
            if (carryingObj != null)
            {
                carryingObj.transform.localScale = new Vector3(Mathf.Abs(carryingObj.transform.localScale.x)* -1, carryingObj.transform.localScale.y, carryingObj.transform.localScale.z);
            }
        }        
    }

    public void PickUpObject()
    {
        //eManager.SetMoveState(PlayerMoveState.pickingUp);
        Physics2D.OverlapCircle(interactiveNode.position, interactiveRange, pickupLayer);
        Collider2D pickupObj = Physics2D.OverlapCircle(interactiveNode.position, interactiveRange, pickupLayer);
        if (pickupObj?.GetComponent<PickableObject>() && pickupObj.transform.parent == null && pickupObj.GetComponent<PickableObject>().CanLift())
        {

            anime.SetTrigger("PickUp");
            anime.SetBool("CarryingObj", true);

            carryingObj = pickupObj.gameObject;

            //disabloi pelaajalta jos tarvetta
            manager.SetCanMove(false);
            manager.SetCanAttack(false);

            carryingObj.transform.SetParent(carryNode);
            carryingObj.GetComponent<PickableObject>().DisableColliderAndSetKinematic();
            carryingObj.transform.DOMove(carryNode.position, pickupSpeed).SetEase(Ease.InFlash).OnComplete(() => FinishedLifting());
        }
       
    }

    //private Collider2D GetPickableObj()
    //{
    //    return Physics2D.OverlapCircle(interactiveNode.position, interactiveRange, pickupLayer);
    //}


    public void FinishedLifting()
    {

        carryingObj.GetComponent<PickableObject>().SetObjectState(PickableObjState.lifted);
        manager.SetCanMove(true);
    }

    private void LowerObject()
    {
        manager.SetCanMove(false);
        anime.SetTrigger("LowerObj");
        anime.SetBool("CarryingObj", false);
        carryingObj?.transform.DOMove(interactiveNode.position, pickupSpeed).SetEase(Ease.InFlash).OnComplete(() => LoweredObj());
       
    }

    private void LoweredObj()
    {
        carryingObj.GetComponent<PickableObject>().SetObjectState(PickableObjState.lowered);

        carryingObj.transform.parent = null;
        carryingObj = null;
        //eManager.SetMoveState(PlayerMoveState.idle);
        canLift = true;
        manager.SetCanAttack(true);
        manager.SetCanMove(true);
    }

    public void DropObj()
    {
        carryingObj.GetComponent<PickableObject>().SetObjectState(PickableObjState.lowered);
        carryingObj.transform.parent = null;
        carryingObj = null;
        canLift = true;

        anime.SetBool("CarryingObj", false);
        manager.SetCanAttack(true);
        manager.SetCanMove(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(interactiveNode.position, interactiveRange);
    }
}
