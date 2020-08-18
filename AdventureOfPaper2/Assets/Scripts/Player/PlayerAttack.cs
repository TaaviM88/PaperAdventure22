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
    private float timeBTWAttack = 0;
    private float startTimeBtwAttack;
    private ITakeDamage<int> enemyToDamage;
    PlayerStats stats;
    public int[] damageArray = { 2, 3, 4, 6, 9, 12, 18, 24 };
    // Start is called before t he first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        anim = GetComponent<PlayerAnimationController>();
        move = GetComponent<Movement>();
        stats = GetComponent<PlayerStats>();
        startTimeBtwAttack = attackCoolDownMultiplayer; 
        timeBTWAttack = attackCoolDownMultiplayer;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && canAttack)
        {
            StartCoroutine(AttackCoolDown());
        }
    }

    public void Attack()
    {
        GameObject enemy = coll.CheckIfStandStabCollide();

       if(enemy != null)
        {
            
            DoDamage(enemy);
        }

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
            //Debug.LogWarning($"Attacking enemy: {enemy.name} Do damage! {damageArray[stats.GetSwordLevel()]} ");
        }
    }

    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(timeBTWAttack);
        canAttack = true;
    }
}
