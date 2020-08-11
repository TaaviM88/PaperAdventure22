using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Levels")]
    public int swordLevel = 1;
    public int maxSwordLevel = 9;

    public int hitpointLevel = 1;
    public int maxHitpointLevel = 9;

    public int magicpointLevel = 1;
    public int maxMagicpointLevel = 9;
    
    public int playerLevel = 1;
    public int maxLevel = 27; 

    [Header("EXP")]
    public int currentEXP = 0;
    public int[] expToNextLevel;
    public int baseEXP = 50;
    public float levelExpGrowthRate = 1.05f;

    [Header("Stats")]
    public int currentHp;
    public int maxHP = 100;
    public float hpGrowthRate = 1.05f;
    public int currentMP;
    public int maxMp = 100;
    public int[] mpLvlBonus;
    public int strength;
    public int defence;

    [Header("Equipment")]
    public int weaponPower;
    public int armorPower;

    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;
        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * levelExpGrowthRate);
        }
    }

    public void AddExp(int expToAdd)
    {
        currentEXP += expToAdd;
        if(playerLevel < maxLevel)
        {
            if(currentEXP > expToNextLevel[playerLevel] && playerLevel < maxLevel)
            {
                currentEXP -= expToNextLevel[playerLevel];
                playerLevel++;

                if(playerLevel % 2== 0)
                {
                    strength++;
                }
                else
                {
                    defence++;
                }

                maxHP = Mathf.FloorToInt(maxHP * hpGrowthRate);
                currentHp = maxHP;

                maxMp += mpLvlBonus[playerLevel];
                currentMP = maxMp;
            }
        }
    }
}
