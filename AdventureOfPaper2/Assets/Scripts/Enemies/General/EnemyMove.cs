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
    [SerializeField]
    AnimationCurve jumpForwardSpeedCurve;
    float jumpHeight;
    float jumpTime;
    float jumpForwardForce;
    float moveSpeed;
    float moveTime;

    Rigidbody2D rb2D;
    EnemyManager manager;
    EnemyAI ai;
    EnemyCollision eCollision;
    EnemyAnimeManager anime;
    EnemyAIState AIState = EnemyAIState.Idling;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        manager = GetComponent<EnemyManager>();
        ai = GetComponent<EnemyAI>();
        eCollision = GetComponent<EnemyCollision>();
        anime = GetComponent<EnemyAnimeManager>();
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
                if (eCollision.onGround)
                {
                    Debug.Log("Mun pitäis liikkua nytten");

                    if (moveTime <= moveSpeedCurve.length)
                    {
                        moveTime += Time.deltaTime;
                        moveSpeed = moveSpeedCurve.Evaluate(moveTime);
                        rb2D.velocity = new Vector2(moveSpeed * manager.side, rb2D.velocity.y);
                    }
                    else
                    {
                        ResetMove();
                    }
                }
                break;
            case EnemyAIState.Jumping:
                
                if(jumpTime <= jumpHeightCurve.length)
                {
                    jumpTime += Time.deltaTime;
                    jumpHeight = jumpHeightCurve.Evaluate(jumpTime);
                    jumpForwardForce = jumpForwardSpeedCurve.Evaluate(jumpTime);

                    rb2D.velocity = new Vector2(jumpForwardForce * manager.side, jumpHeight);
                    //rb2D.AddForce(new Vector2(10 * manager.side, jumpHeight));

                }
                else
                {
                    if(eCollision.onGround)
                    {
                        ResetJump();
                        ResetMove();
                        ai.SetAIState(EnemyAIState.Idling);
                    }
                  
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

        anime.SetBool("OnGround", eCollision.onGround);
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

    public void SetAiState(EnemyAIState newState)
    {
        AIState = newState;
    }

}
