using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallKey : MonoBehaviour, ITakeDamage<int>
{
    public int id = 0;

    public void Damage(int Damage)
    {
        Debug.Log("Avain perkele");
        PalaceManager.instance.RemoveItem(id,true);
        PlayerManager.instance.AddSmallKeyToInventory();
        
        Destroy(gameObject);
    }
}
