using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IUnit
{
    [Header("Levels")]
    public int swordLevel = 1;
    public int maxSwordLevel = 8;

    public int HPLevel = 1;
    public int maxHitpointLevel = 8;

    public int magicLevel = 1;
    public int maxMagicpointLevel = 8;
    
    public int playerLevel = 1;
    public int maxLevel = 27; 

    [Header("EXP")]
    public int currentEXP = 0;
    public int[] expToNextLevel;
    public int baseEXP = 50;
    public float levelExpGrowthRate = 1.05f;

    [Header("Stats")]

    public float hpGrowthRate = 1.05f;

    public int[] mpLvlBonus;
    public int strength;
    public int defence;

    public int heartContainer = 4;
    public int magicBottle = 4;

    [Header("Equipment")]
    public int weaponPower;
    public int armorPower;

    PlayerManager pManager;

    // Start is called before the first frame update
    void Awake()
    {
        
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;
        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * levelExpGrowthRate);
        }

        //UpdateUIStats();
        pManager = GetComponent<PlayerManager>();
        if (pManager == null)
        {
           Debug.LogError("Mitä helvettiä!");
        }

    }


    public void AddExp(int expToAdd)
    {
        Debug.Log("lisätään exp:tä " + expToAdd);
        currentEXP += expToAdd;
        if (playerLevel < maxLevel)
        {
            if (currentEXP >= expToNextLevel[playerLevel] && playerLevel < maxLevel)
            {
                currentEXP -= expToNextLevel[playerLevel];
                playerLevel++;

                //if(playerLevel % 2== 0)
                //{
                //    strength++;
                //}
                //else
                //{
                //    defence++;
                //}

                //maxHP = Mathf.FloorToInt(maxHP * hpGrowthRate);


                //maxMp += mpLvlBonus[playerLevel];

                //SetLevel(playerLevel + 1);
                //SetSwordlLevel(swordLevel + 1);
                //SetHPLevel(HPLevel + 1);
                //SetMagicLevel(magicLevel + 1);
                MasterCanvasManager.instance.ShowLevelUpUI();

            }
        }

        pManager?.UpdateUIStats();
    }

    

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public int GetLevel()
    {
        return playerLevel;
    }

    public void SetLevel(int currentLevel)
    {
        playerLevel = currentLevel;
        pManager.UpdateUIStats();
    }

    public int GetSwordLevel()
    {
        return swordLevel;
    }

    public void SetSwordlLevel(int currentLevel)
    {
        swordLevel = currentLevel;
        pManager.UpdateUIStats();
    }

    public int GetMagicLevel()
    {
        return magicLevel;
    }

    public void SetMagicLevel(int currentMagicLevel)
    {
        magicLevel = currentMagicLevel;
        pManager.UpdateUIStats();
    }

    public int GetHPLevel()
    {
        return HPLevel;
    }

    public void SetHPLevel(int currentHpLevel)
    {
        HPLevel = currentHpLevel;
        pManager.UpdateUIStats();
    }

    public int GetHeartContainerAmount()
    {
        return heartContainer;
    }

    public void SetHeartContainerAmount(int currentHeartContainerAmount)
    {
        heartContainer = currentHeartContainerAmount;
        pManager.UpdateUIStats();
    }

    public int GetMagicBottleAmount()
    {
        return magicBottle;
    }

    public void SetMagicBottleAmount(int currentBottleAmount)
    {
        magicBottle = currentBottleAmount;
        pManager.UpdateUIStats();
    }
}
