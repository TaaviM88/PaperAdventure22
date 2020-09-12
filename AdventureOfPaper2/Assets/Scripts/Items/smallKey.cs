using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallKey : MonoBehaviour, ITakeDamage<int>, ICollectables
{
    public int id = 0;
    bool _isCollected;
    public void Damage(int Damage)
    {
        Debug.Log("Avain perkele");
        PalaceManager.instance.RemoveItem(id,true);
        PlayerManager.instance.AddSmallKeyToInventory();
        SetCollectable(true);
        //DestroyObject();
    }

    public bool IsCollected()
    {
        return _isCollected;
    }

    public void SetCollectable(bool collected)
    {
        _isCollected = collected;
        if(_isCollected)
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
