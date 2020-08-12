using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerStats stats;

    ////One bar of your magic/heart meter = 16 points
    public int heartContainerInHp = 16;
    public int magicBottleInHp = 16;
    int currentHp;
    int maxHp;

    int currentMp;
    int maxMp;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();

        maxHp = stats.GetHeartContainerAmount() * heartContainerInHp;
        maxMp = stats.GetMagicBottleAmount() * magicBottleInHp;

        currentHp = maxHp;
        currentMp = maxMp;

        Debug.Log($"current HP {currentHp}/max HP{maxHp} n/ current MP {currentMp} / Max Mp {maxMp}");
    }

}
