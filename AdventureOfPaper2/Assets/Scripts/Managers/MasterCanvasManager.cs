using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MasterCanvasManager : MonoBehaviour
{
    public static MasterCanvasManager instance;
    public GameObject LevelUpUI;
    public GameObject itemAndSpell;
    public TMP_Text swordStat;
    public TMP_Text magicStat;
    public TMP_Text lifeStat;
    public TMP_Text ExpStat;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        if(LevelUpUI.activeInHierarchy)
        {
            LevelUpUI.SetActive(false);
        }

        if(itemAndSpell.activeInHierarchy)
        {
            itemAndSpell.SetActive(false);
        }
    }

    public void UpdateStats(int sword, int magic, int mp, int maxMp, int life, int hp, int maxHp, int currenttexp, int toNextLevelExp)
    {
        swordStat.text = $"Sword: {sword}";
        magicStat.text = $"Magic: {magic} : {mp} / {maxMp}";
        lifeStat.text = $"Life: {life} : {hp} / {maxHp}";
        ExpStat.text = $"Next {currenttexp} / {toNextLevelExp}";
    }

    public void ShowLevelUpUI()
    {
        LevelUpUI.SetActive(true);
    }

    public void HideLevelUpUI()
    {
        if(LevelUpUI.activeInHierarchy)
        {
            LevelUpUI.SetActive(false);
        }
    }

    public void ToggleItemAndSpellUI()
    {
        if(!itemAndSpell.activeInHierarchy)
        {
            itemAndSpell.SetActive(true);
        }
        else
        {
            itemAndSpell.SetActive(false);
        }
    }
}
