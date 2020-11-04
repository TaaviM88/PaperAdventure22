using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimeBehaviour : StateMachineBehaviour
{
    PlayerEnumManager pEnums;
    PlayerAttack attack;
    public PlayerMoveState state = PlayerMoveState.idle;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(state == PlayerMoveState.idle)
        {
            attack = animator.GetComponent<PlayerAttack>();
            attack.ResetAttackCombo();
        }

        if (state == PlayerMoveState.attack)
        {
            attack = animator.GetComponent<PlayerAttack>();

            animator.SetBool("Attack2Bool", false);
            animator.SetBool("Attack3Bool", false);
            attack.SetBooleans(false, false, false,true);
            //animator.GetComponent<PlayerAttack>().ResetAttackCombo();


            //pEnums = animator.GetComponent<PlayerEnumManager>();
            //pEnums.SetMoveState(state);

            //animator.GetComponent<PlayerAttack>().ResetAttackCombo();
        }
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      if(state == PlayerMoveState.attack)
       {
            animator.SetBool("Attack2Bool", false);
            animator.SetBool("Attack3Bool", false);
            
            AnimatorTransitionInfo currentTransition = animator.GetAnimatorTransitionInfo(0);
            Debug.Log("test: " + currentTransition.nameHash);
            if(currentTransition.IsName("Base.stab_right -> Base Layer.idle_right"))
            {
                
                attack.SetBooleans(true, true, true, false);
                attack.ResetAttackCombo();
            }
             else if(currentTransition.IsName("Base.stab_right 0 -> Base Layer.idle_right"))
             {
                attack.ResetAttackCombo();
                attack.SetBooleans(true, true, true, false);
              }
            else if (currentTransition.IsName("Base.stab_right 1 -> Base Layer.idle_right"))
            {

                attack.ResetAttackCombo();
                attack.SetBooleans(true, true, true, false);
            }

            //      var transitionInfo = animator.GetAnimatorTransitionInfo(layerIndex);

            //      Debug.LogError(transitionInfo.userNameHash);
            //      if(transitionInfo.userNameHash == 0)
            //      {
            //          animator.GetComponent<PlayerAttack>().ResetAttackCombo();

        }
      
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
