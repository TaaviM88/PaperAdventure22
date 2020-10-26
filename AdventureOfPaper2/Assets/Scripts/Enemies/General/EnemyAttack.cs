using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Common;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [Header("Parameters")]
    public Transform attackpos;

    public float attackRange = 0.5f, attackCooldown = 1f;

    public int damage = 1; 

    public LayerMask playerLayer;

    public EnemyAttackType attackType = EnemyAttackType.Circle;

    bool canAttack = true;

    [Header("Polish")]
    public ParticleSystem attackParcticle;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if(canAttack)
        {
            Collider2D playerToDamage = Physics2D.OverlapCircle (attackpos.position, attackRange, playerLayer);

            playerToDamage?.GetComponent<PlayerManager>().Damage(damage);

            StartCoroutine(AttackCooldown());
            //Osuu useampaan
            //Collider2D[] playersToDamage = Physics2D.OverlapCircleAll(attackpos.position, attackRange, Players);
            //for (int i = 0; i < playersToDamage.Length; i++)
            //{
            //    playersToDamage[i].GetComponent<PlayerManager>().Damage(damage);

            //    StartCoroutine(AttackDownCoolDown());
            //}
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public bool GetCanAttack()
    {
        return canAttack;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.;
        
        switch (attackType)
        {
            case EnemyAttackType.Box:
                //Gizmos.DrawWireCube()
                break;
            case EnemyAttackType.Circle:
                Gizmos.DrawWireSphere(attackpos.position, attackRange);
                break;
            default:
                break;
        }
    }
}
