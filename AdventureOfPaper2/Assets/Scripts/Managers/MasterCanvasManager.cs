using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MasterCanvasManager : MonoBehaviour
{
    public static MasterCanvasManager instance;
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
    }

    public void UpdateStats(int sword, int magic, int mp, int maxMp, int life, int hp, int maxHp, int currenttexp, int toNextLevelExp)
    {
        swordStat.text = $"Sword: {sword}";
        magicStat.text = $"Magic: {magic} : {mp} / {maxMp}";
        lifeStat.text = $"Life: {life} : {hp} / {maxHp}";
        ExpStat.text = $"Next {currenttexp} / {toNextLevelExp}";
    }

}
