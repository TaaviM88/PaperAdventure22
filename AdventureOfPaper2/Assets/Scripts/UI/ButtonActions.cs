using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActions : MonoBehaviour
{
    PlayerStats stats;
    public void Start()
    {
        FindPlayer();
    }

    public void FindPlayer()
    {
        stats = FindObjectOfType<PlayerStats>();
    }

    public void HideLevelUpWindow()
    {
        MasterCanvasManager.instance.HideLevelUpUI();
    }

    #region LevelUpActions
    public void RiseSwordLevel()
    {
       
        if(stats == null)
        {
            FindPlayer();
        }

        stats?.SetSwordlLevel(stats.GetSwordLevel() + 1);
        stats?.LeveledUp();
        HideLevelUpWindow();
    }

    public void RiseHPLevel()
    {
        if (stats == null)
        {
            FindPlayer();
        }

        stats?.SetHPLevel(stats.GetHPLevel() + 1);
        stats?.LeveledUp();
        HideLevelUpWindow();
    }

    public void RiseMagicLevel()
    {
        if (stats == null)
        {
            FindPlayer();
        }
        stats?.SetMagicLevel(stats.GetMagicLevel() + 1);
        stats?.LeveledUp();
        HideLevelUpWindow();
    }

    public void CancelWindow()
    {
        HideLevelUpWindow();
    }
    #endregion

    #region SpellActions

    #endregion
}
