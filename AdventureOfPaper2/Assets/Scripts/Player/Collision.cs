using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer, enemyLayer;
    [Space]
    public bool onGround, onWall, onRightWall, onLeftWall, onCeiling, stabHit;
    public int wallSide;
    public Transform stabRight, stabLeft, downStab, upStab;
    public Vector3 stabRightBoxSize = new Vector3(0f,0f,0f), stabLeftBoxSize = new Vector3(0f,0f,0f);
    public float stabDistance = 0.5f;
    public float downStabRadius = 1f, upStabRadius = 1f;

    [Space]
    [Header("Collision")]
    public float collisionRadius = 0.25f;
    //public Vector2 collisionSize;
    public Vector2 bottomOffset, rightOffset, leftOffset,ceilingOffset;
    private Color debugCollisionColor = Color.red;
    private Vector2 originalOffSetRight, originalOffSetLeft, originalCapsuleOffSet, originalOffSetCeiling, originalOffSetBottom;
    Movement move;
    // Start is called before the first frame update
    void Start()
    {
        originalOffSetRight = rightOffset;
        originalOffSetLeft = leftOffset;
        originalOffSetCeiling = ceilingOffset;
        originalOffSetBottom = bottomOffset;
        move = GetComponent<Movement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onCeiling = Physics2D.OverlapCircle((Vector2)transform.position + ceilingOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) ||
      Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer); 
        wallSide = onRightWall ? -1 : 1;
    }

    public GameObject CheckIfStandStabCollide()
    {
        RaycastHit2D hit2D;
        //stabHit = Physics2D.BoxCast(Stab.position, (Vector2)stabBoxSize, 0f, Vector2.right, enemyLayer); 
        if (move.side == 1)
        {
             hit2D = Physics2D.BoxCast(stabRight.position, stabRightBoxSize, 0f, transform.right, stabDistance, enemyLayer);
        }
        else
        {
            hit2D = Physics2D.BoxCast(stabLeft.position, stabLeftBoxSize, 0f, -transform.right, stabDistance, enemyLayer);
        }

           
        return hit2D.collider?.gameObject;
    }


    public GameObject CheckIfDownStabCollide()
    {
        RaycastHit2D hit2D;

        hit2D = Physics2D.CircleCast(downStab.position, downStabRadius, -transform.up, stabDistance,enemyLayer);

        return hit2D.collider?.gameObject;
    }


    public GameObject CheckIfUpStabCollide()
    {
        RaycastHit2D hit2D;

        hit2D = Physics2D.CircleCast(upStab.position, upStabRadius, transform.up, stabDistance, enemyLayer);

        return hit2D.collider?.gameObject;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var positions = new Vector2[] { bottomOffset, ceilingOffset, rightOffset, leftOffset };
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + ceilingOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);

        Gizmos.DrawWireCube(stabRight.position, stabRightBoxSize);
        Gizmos.DrawWireCube(stabLeft.position, stabLeftBoxSize);

        Gizmos.DrawWireSphere(downStab.position, downStabRadius);
        Gizmos.DrawWireSphere(upStab.position, upStabRadius);
    }



    public bool IsEverythingColliding()
    {
        if (onGround && onRightWall && onLeftWall && onCeiling)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
