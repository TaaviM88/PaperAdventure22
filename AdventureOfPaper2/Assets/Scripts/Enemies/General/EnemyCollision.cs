using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;
    
    [Space]
    public bool onGround, onWall, onFrontWall, onBackWall, onCeiling;
    public int wallSide;
    
    [Space]
    [Header("Collision")]
    public float collisionRadius = 0.1f;

    public Vector2 bottomOffset, frontOffset, backOffset, ceilingOffset;
    private Color debugCollisionColor = Color.red;
    private Vector2 originalOffsetFront, originalOffsetBack,
        originalCapsuleOffset, originalOffsetCeiling, originalOffsetBottom;

    
    // Start is called before the first frame update
    void Start()
    {
        originalOffsetFront = frontOffset;
        originalOffsetBack = backOffset;
        originalOffsetCeiling = ceilingOffset;
        originalOffsetBottom = bottomOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onCeiling = Physics2D.OverlapCircle((Vector2)transform.position + ceilingOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + frontOffset, collisionRadius, groundLayer) ||
            Physics2D.OverlapCircle((Vector2)transform.position + backOffset, collisionRadius, groundLayer);
        onFrontWall = Physics2D.OverlapCircle((Vector2)transform.position + frontOffset, collisionRadius, groundLayer);
        onBackWall = Physics2D.OverlapCircle((Vector2)transform.position + backOffset, collisionRadius, groundLayer);
        wallSide = onFrontWall ? -1 : 1;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + ceilingOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + frontOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + backOffset, collisionRadius);
    }
}
