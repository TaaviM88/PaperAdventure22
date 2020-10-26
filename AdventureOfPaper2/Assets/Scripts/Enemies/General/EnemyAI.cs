using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [Header("Parameters")]
    public LayerMask playerLayer;
    public EnemyType type = EnemyType.standing;
    public float targetRange = 3;
    public float actionCooldown = 0.5f;

    bool canDoAction = true;

    Vector3 startingPosition;
    Vector3 roamPosition;
    
    EnemyPathfinding pathfind;
    EnemyAttack eAttack;
    EnemyMove eMove;
    EnemyManager eManager;
    EnemyAnimeManager anime;
    EnemyCollision eCollision;
    public EnemyAIState aiState = EnemyAIState.Idling;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;

        pathfind = GetComponent<EnemyPathfinding>();
        anime = GetComponent<EnemyAnimeManager>();
        eAttack = GetComponent<EnemyAttack>();
        eMove = GetComponent<EnemyMove>();
        eManager = GetComponent<EnemyManager>();
        eCollision = GetComponent<EnemyCollision>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        switch (aiState)
        {
            case EnemyAIState.Spawning:
                break;
            case EnemyAIState.Idling:
                
                if (canDoAction)
                {
                    anime.SetTrigger("Idle");
                    eMove.SetAiState(aiState);
                    //tarkkaillaan jos  pelaaja tulee meidän target alueen sisään.
                   if( CheckIfPlayerIsClose())
                    {
                        aiState = EnemyAIState.ChasingTarget;
                    }

                    StartCoroutine(ActionCooldown());
                }
                break;
            case EnemyAIState.Roaming:

                break;
            case EnemyAIState.ChasingTarget:

                if(canDoAction)
                {
                   if(LookAtPlayer())
                    {
                        anime.SetTrigger("Move");
                        aiState = EnemyAIState.MovingForward;      
                    }

                    StartCoroutine(ActionCooldown());
                }

                break;
            case EnemyAIState.MovingForward:
                if(canDoAction)
                {
                    eMove.SetAiState(aiState);


                    if(eAttack.CheckIfCanAttack() && eCollision.onGround)
                    {
                        anime.SetTrigger("Attack");
                        aiState = EnemyAIState.Jumping;
                    }


                    //anime.SetTrigger("Move");
                    if(!LookAtPlayer())
                    {
                        if (!CheckIfPlayerIsClose())
                        {

                            aiState = EnemyAIState.Idling;
                        }
                    }

                    StartCoroutine(ActionCooldown());
                }

                
                break;
            case EnemyAIState.Jumping:
                //check if on ground
                if(canDoAction)
                {
                    eMove.SetAiState(aiState);
                    StartCoroutine(ActionCooldown());
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

    }

    public void SetAIState(EnemyAIState newState)
    {
        aiState = newState;
    }

    public EnemyAIState GetAIState()
    {
        return aiState;
    }

    IEnumerator ActionCooldown()
    {
        canDoAction = false;
        yield return new WaitForSeconds(actionCooldown);
        canDoAction = true;
    }  


    public bool CheckIfPlayerIsClose()
    {
        if (Physics2D.OverlapCircle((Vector2)transform.position, targetRange, playerLayer))
        {
            return true;
        }
       
        return false;
    }

    public bool LookAtPlayer()
    {
        Vector3 raycastStartPoint = transform.position + new Vector3(0, 0, 0);
        RaycastHit2D hit = Physics2D.Raycast(raycastStartPoint, transform.localScale.x * transform.right, targetRange,playerLayer);
        
        if(hit.collider?.tag == "Player")
        {
            Debug.Log("Näen pelaajan ^_^");
            return true;
        }
        else
        {
            Debug.Log(" En näe pelaaja ( ´ﾟДﾟ`）");
            eManager.FlipEnemy();
            StartCoroutine(ActionCooldown());
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.localScale.x * transform.right);
        //Gizmos.DrawWireSphere((Vector2)transform.position, targetRange);
    }
}
