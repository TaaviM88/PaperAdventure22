using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxBoss : BossManager
{

    public override void Damage(int damage)
    {
        base.Damage(damage);
        anime.SetTrigger("TakeDamage");
    }

    protected override void onInitializeBoss(int id)
    {
        if (id == this.bossRoomId)
        {
            Debug.LogError("Hei nouse pönttö");
            anime.SetTrigger("Get_Up");
        }
        
    }

    protected override bool DoRaycast()
    {
        return base.DoRaycast();
    }

    protected override void DoAction()
    {
        base.DoAction();
         float playerDistance = CheckPlayerDistance();

        if(playerDistance <= 1 && playerDistance > 0)
        {
            //Do Close Melee attack
            anime.SetBool("Attack1_Charge", true);
        }

        if(playerDistance <= 2 && playerDistance > 1)
        {
            //Do FlameThrower
            anime.SetTrigger("Attack3");
        }

        if(playerDistance > 2)
        {
            //Do Roll attack
            anime.SetBool("Is_Rolling", true);
            anime.SetTrigger("Attack2");
           
        }

        state = BossState.Attacking;
    }

    public override void Die()
    {
        base.Die();
        anime.SetTrigger("Die");
        DisableAllCollisions();
    }
}
