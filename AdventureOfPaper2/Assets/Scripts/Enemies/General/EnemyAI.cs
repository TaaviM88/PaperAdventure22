using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [Header("Parameters")]
    public LayerMask playerLayer;
    public EnemyType type = EnemyType.standing;
    public float targetRange = 3;


    Vector3 startingPosition;
    Vector3 roamPosition;
    
    EnemyPathfinding pathfind;
    EnemyAttack eAttack;
    EnemyMove eMove;

    Animator anime;
    
    public EnemyAIState aiState = EnemyAIState.Idling;
    
    // Start is called before the first frame update
    void Start()
    {
        pathfind = GetComponent<EnemyPathfinding>();
        anime = GetComponent<Animator>();
        eAttack = GetComponent<EnemyAttack>();
        eMove = GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        switch (aiState)
        {
            case EnemyAIState.Spawning:
                break;
            case EnemyAIState.Idling:
                //tarkkaillaan jos  pelaaja tulee meidän target alueen sisään.
                if(Physics2D.OverlapCircle((Vector2)transform.position,targetRange,playerLayer))
                {
                    aiState = EnemyAIState.ChasingTarget;
                }
                break;
            case EnemyAIState.Roaming:

                break;
            case EnemyAIState.ChasingTarget:
                break;
            case EnemyAIState.MovingForward:
                break;
            case EnemyAIState.Jumping:
                //check if on ground
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere((Vector2)transform.position, targetRange);
    }
}
