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
    #endregion

    #region Player stats
    int GetLevel();
    void SetLevel(int currentLevel);

    //set sword Level
    int GetSwordLevel();
    void SetSwordlLevel(int currentLevel);

    //set magic Level
    int GetMagicLevel();
    void SetMagicLevel(int currentMagicLevel);
    //set HP level
    int GetHPLevel();
    void SetHPLevel(int currentHpLevel);

    //set Heart container amount
    int GetHeartContainerAmount();
    void SetHeartContainerAmount(int currentHeartContainerAmount);

    // set magic bottle amount
    int GetMagicBottleAmount();
    void SetMagicBottleAmount(int currentBottleAmount);
    #endregion

    //Not done yet
    #region Player acquired stuff
   
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

public interface Spells
{
    #region spells
    //Shield Mp: 32 24 	24 	16 	16 	16 	16 	16 
    bool GetShieldSpell();
    void SetShieldSpell(bool has);
    //Jump Mp: 48 	40 	32 	32 	20 	16 	12 	8 
    bool GetJumpSpell();
    void SetJumpSpell(bool has);
    //Life Mp: 70 	70 	60 	60 	50 	50 	50 	50 
    bool GetLifeSpell();
    void SetLifeSpell(bool has);
    //Fairy Mp: 80 	80 	60 	60 	40 	40 	40 	40 
    bool GetFairySpell();
    void SetFairySpell(bool has);
    //Fire Mp: 120 	80 	60 	30 	16 	16 	16 	16 
    bool GetFireSpell();
    void SetFireSpell(bool has);
    //Reflect Mp: 120 	120 	80 	48 	40 	32 	24 	16 
    bool GetReflectSpell();
    void SetReflect(bool has);
    //Spell Mp: 120 	112 	98 	80 	48 	32 	24 	16 
    bool GetSpellSpell();
    void SetSpellSpell(bool has);
    //Thunder Mp: 120 	120 	120 	120 	120 	120 	100 	64
    bool GetThunderSpell();
    void SetThunderSpell(bool has);
    #endregion
}

public interface IProgress
{
    void SetScene(string sceneName);
    string GetScene();

}