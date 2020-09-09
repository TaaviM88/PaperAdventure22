using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int smallKeyAmount = 0;
    //spells
    //keyItems

    public void AddSmallKey()
    {
        smallKeyAmount++;
    }

    public void RemoveSmallKey()
    {
        smallKeyAmount--;
    }

    public int GetSmallKeyAmount()
    {
        return smallKeyAmount;
    }
}
