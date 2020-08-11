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
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        anim = GetComponent<PlayerAnimationController>();
        move = GetComponent<Movement>();
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
            Debug.Log($"Attacking enemy: {enemy.name} Do damage! ");
        }

    }

    public void DownAttack()
    {
        GameObject enemy = coll.CheckIfDownStabCollide();
        if(enemy !=null)
        {
            Debug.Log($"DOWN STAB! Attacking enemy: {enemy.name} DO DAMAGE!");
            move.DoJump();
        }
    }

    public void UpAttack()
    {
        GameObject enemy = coll.CheckIfUpStabCollide();
        if(enemy != null)
        {
            Debug.Log($"UP Stab! Enemy: { enemy.name}  do damage");
        }
    }

    public void DuckStab()
    {
        GameObject enemy = coll.CheckIfDuckStabCollide();
        if (enemy != null)
        {
            Debug.Log($"Duck Stab! Enemy: { enemy.name}  do damage");
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
