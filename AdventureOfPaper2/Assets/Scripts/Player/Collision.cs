using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;
    [Space]
    public bool onGround, onWall, onRightWall, onLeftWall, onCeiling;
    public int wallSide;

    [Space]
    [Header("Collision")]
    public float collisionRadius = 0.25f;
    //public Vector2 collisionSize;
    public Vector2 bottomOffset, rightOffset, leftOffset,ceilingOffset;
    private Color debugCollisionColor = Color.red;
    private Vector2 originalOffSetRight, originalOffSetLeft, originalCapsuleOffSet, originalOffSetCeiling, originalOffSetBottom;

    // Start is called before the first frame update
    void Start()
    {
        originalOffSetRight = rightOffset;
        originalOffSetLeft = leftOffset;
        originalOffSetCeiling = ceilingOffset;
        originalOffSetBottom = bottomOffset;
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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var positions = new Vector2[] { bottomOffset, ceilingOffset, rightOffset, leftOffset };
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + ceilingOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
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
