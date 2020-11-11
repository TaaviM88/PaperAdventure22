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

    public override void Die()
    {
        base.Die();
        anime.SetTrigger("Die");
        DisableAllCollisions();
    }
}
