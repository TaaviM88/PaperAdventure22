﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackPower = 1;
    public float attackCoolDownMultiplayer = 2;
    private bool canAttack = true;
    private bool animeOn = false;
    Collision coll;
    Movement move;
    PlayerAnimationController anim;
    PlayerManager manager;
    PlayerEnumManager enumManager;
    private float timeBTWAttack = 0;
    private float startTimeBtwAttack;
    private ITakeDamage<int> enemyToDamage;

    int comboAttackCount = 0;
    PlayerStats stats;
    public int[] damageArray = { 2, 3, 4, 6, 9, 12, 18, 24 };

    [Header("Axe Parameters")]
    public int axeAttackPower = 18;
    // Start is called before t he first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        anim = GetComponent<PlayerAnimationController>();
        move = GetComponent<Movement>();
        stats = GetComponent<PlayerStats>();
        manager = GetComponent<PlayerManager>();
        enumManager = GetComponent<PlayerEnumManager>();

        startTimeBtwAttack = attackCoolDownMultiplayer; 
        timeBTWAttack = attackCoolDownMultiplayer;
    }

    // Update is called once per frame
    void Update()
    {
        if(!manager.GetCanAttack())
        {
            return;
        }

        if(Input.GetButtonDown("Fire1") && canAttack)
        {
            TriggerAttackAnim();
            //SetBooleans(false, false, true, false);
            //StartCoroutine(AttackCoolDown());
            //move.SetVelocityZero();
        }
        // else if(Input.GetButtonDown("Fire1") && animeOn)
        //{
        //    TriggerAttackAnim();
        //}

        //axe hyökkäys
        if(Input.GetButtonDown("Fire3") )
        {
            
            SetBooleans(false, false, false,false);

            anim.SetTrigger("AxeCharge");
            move.SetVelocityZero();
        }

        //Pannu hyökkäys
        if (Input.GetButtonDown("Fire4"))
        {

            SetBooleans(false, false, false, false);

            anim.SetTrigger("PanCharge");
            move.SetVelocityZero();
        }


        if (Input.GetButtonUp("Fire3"))
        {
            anim.SetTrigger("AxeAttack");

//            ResetBooleans();
        }

        if (Input.GetButtonUp("Fire4"))
        {
            anim.SetTrigger("PanAttack");

          
        }

        //Debug.Log($"Combo: {comboAttackCount}");
    }

    public void Attack()
    { 
       GameObject enemy = coll.CheckIfStandStabCollide();
        
       if(enemy != null)
        {
            DoDamage(enemy);
        }

        ResetBooleans();
    }

    public void DownAttack()
    {
        GameObject enemy = coll.CheckIfDownStabCollide();
        if(enemy !=null)
        {
            DoDamage(enemy);
            move.DoJump();
        }
    }

    public void UpAttack()
    {
        GameObject enemy = coll.CheckIfUpStabCollide();
        if(enemy != null)
        {
            DoDamage(enemy);
        }
    }

    public void DuckStab()
    {
        GameObject enemy = coll.CheckIfDuckStabCollide();
        if (enemy != null)
        {
            DoDamage(enemy);
        }
    }


    public void DoDamage(GameObject enemy)
    {
        enemyToDamage = enemy.GetComponent<ITakeDamage<int>>();
        if(enemyToDamage != null)
        {
            enemyToDamage.Damage(damageArray[stats.GetSwordLevel()]);
            Debug.LogWarning($"Attacking enemy: {enemy.name} Do damage! {damageArray[stats.GetSwordLevel()]} ");
        }
    }


    public void AxeAttack()
    {
        Collider2D[] objects = coll.CheckIfAxeCollide();

        if(objects != null)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                Debug.Log($"Osuin seuraaviin objekteihin: {objects[i]}");
                //temp osumis setup
                objects[i]?.GetComponent<ITakeDamage<int>>().Damage(axeAttackPower);

            }
        }
    }

    public void PanAttack()
    {
        Debug.Log("PAN ATTACK");
    }


    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        TriggerAttackAnim();
        yield return new WaitForSeconds(timeBTWAttack);
        canAttack = true;
    }

    public void TriggerAttackAnim()
    {
        if( PlayerMoveState.duck == enumManager.GetMoveState())
        {
            anim.SetTrigger("DuckAttack");
        }
        switch (comboAttackCount)
        {
            case 0:
                if(coll.onGround)
                {
                    move.SetVelocityZero();
                }
                
                anim.SetTrigger("Attack");               
                break;
            case 1:
                if (coll.onGround)
                {
                    move.SetVelocityZero();
                }

                //anim.SetTrigger("Attack2");
                anim.SetBool("Attack2Bool", true);
                break;
            case 2:
                if (coll.onGround)
                {
                    move.SetVelocityZero();
                }

                //anim.SetTrigger("Attack3");
                anim.SetBool("Attack3Bool", true);
                break;
        }

        if(comboAttackCount > 2)
        {
            ResetAttackCombo();
        }
        else
        {
            comboAttackCount++;
        }

    }

    public void ResetAttackCombo()
    {
        comboAttackCount = 0;
        anim.SetBool("Attack2Bool", false);
        anim.SetBool("Attack3Bool", false);
    }

    public void ResetBooleans()
    {
        manager.SetCanLift(true);
        manager.SetCanMove(true);
        canAttack = true;
    }

    public void SetBooleans(bool canlift, bool canmove, bool canattack, bool isAnimeOn)
    {
        manager.SetCanLift(canlift);
        manager.SetCanMove(canmove);
        canAttack = canattack;
        animeOn = isAnimeOn;
    }
}
