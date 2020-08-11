using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ITakeDamage<T>
{
    void Damage(T Damage);
}

public interface IDie
{
    void Die();
}

public interface ISpawnerID<T>
{
    void SetSpawnerID(T newID);
}

public interface IUnit
{
    #region test stuff
    Vector3 GetPosition();
    void SetPosition(Vector3 position);
    int GetGoldAmount();
    void SetGoldAmount(int goldAmount);
    #endregion
    //set scene

    #region Player stats
    //set level
    //set sword Level
    //set magic Level
    //set HP level
    //set Heart container amount
    // set magic bottle amount
    #endregion
    #region Player acquired stuff
    //spells

    //Items:

    //Quest items:
    //Collectables
    #endregion
    #region Player Progress
    //
    // läpäistyjen Temppelien määrä
    // avainten määrä
    // 
    #endregion
}