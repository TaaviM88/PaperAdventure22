using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{

    [SerializeField] 
    AnimationCurve moveSpeedCurve;
    [SerializeField]
    AnimationCurve jumpHeightCurve;

    float jumpHeight;
    float jumpTime;

    float moveSpeed;
    float moveTime;

    Rigidbody2D rb2D;
    EnemyManager manager;
    EnemyAI ai;
    EnemyAIState AIState = EnemyAIState.Idling;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        manager = GetComponent<EnemyManager>();
        ai = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        AIState = ai.GetAIState();
        switch (AIState)
        {
            case EnemyAIState.Spawning:
                break;
            case EnemyAIState.Idling:
                break;
            case EnemyAIState.Roaming:
                break;
            case EnemyAIState.ChasingTarget:
                break;
            case EnemyAIState.MovingForward:
                break;
            case EnemyAIState.Jumping:
                if(jumpTime <= jumpHeightCurve.length)
                {
                    jumpTime += Time.deltaTime;
                    jumpHeight = jumpHeightCurve.Evaluate(jumpTime);
                    rb2D.velocity = new Vector2(rb2D.velocity.x, jumpHeight);
                }
                else
                {
                    ResetJump();
                    ResetMove();
                }
                break;
            case EnemyAIState.Attacking:
                break;
            case EnemyAIState.TakingDamage:
                break;
            case EnemyAIState.Dying:
                break;
            default:
                break;
        }

        if(moveTime <= moveSpeedCurve.length)
        {
            moveTime += Time.deltaTime;
            moveSpeed = moveSpeedCurve.Evaluate(moveTime);
            rb2D.velocity = new Vector2(moveSpeed * manager.side, rb2D.velocity.y);
        }

    }

    public void ResetJump()
    {
        jumpHeight = jumpHeightCurve.Evaluate(0);
        jumpTime = 0f;
    }

    public void ResetMove()
    {
        moveSpeed = moveSpeedCurve.Evaluate(0);
        moveTime = 0;
    }

   public void DoJump()
    {

    }
}
