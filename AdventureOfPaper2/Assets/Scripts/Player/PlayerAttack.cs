using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackPower = 1;
    public float attackCoolDownMultiplayer = 2;
    private bool canAttack = true;
    Collision coll;
    Movement move;
    PlayerAnimationController anim;
    PlayerManager manager;
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

            SetBooleans(false, false, true);
            StartCoroutine(AttackCoolDown());
            move.SetVelocityZero();
        }

        if(Input.GetButtonDown("Fire3") )
        {
            
            SetBooleans(false, false, false);

            anim.SetTrigger("AxeCharge");
            move.SetVelocityZero();
        }

        if(Input.GetButtonUp("Fire3"))
        {
            anim.SetTrigger("AxeAttack");

//            ResetBooleans();
        }
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

    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        TriggerAttackAnim();
        yield return new WaitForSeconds(timeBTWAttack);
        canAttack = true;
    }

    public void TriggerAttackAnim()
    {
        switch (comboAttackCount)
        {
            case 0:
                anim.SetTrigger("Attack");               
                break;
            case 1:
                //anim.SetTrigger("Attack2");
                anim.SetBool("Attack2Bool", true);
                break;
            case 2:
                anim.SetTrigger("Attack3");
                anim.SetBool("Attack3Bool", true);
                break;
        }

        if(comboAttackCount >=3)
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

    public void SetBooleans(bool canlift, bool canmove, bool canattack)
    {
        manager.SetCanLift(canlift);
        manager.SetCanMove(canmove);
        canAttack = canattack;
    }
}
