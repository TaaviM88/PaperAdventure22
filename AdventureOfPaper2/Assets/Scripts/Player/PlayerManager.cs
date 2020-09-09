using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  Cinemachine;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance; 
    PlayerStats stats;
    PlayerInventory inventory;
    ////One bar of your magic/heart meter = 16 points
    public int heartContainerInHp = 16;
    public int magicBottleInHp = 16;
    int currentHp;
    int maxHp;

    int currentMp;
    int maxMp;
    bool initialCheck = false;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        inventory = GetComponent<PlayerInventory>();
        maxHp = stats.GetHeartContainerAmount() * heartContainerInHp;
        maxMp = stats.GetMagicBottleAmount() * magicBottleInHp;

        currentHp = maxHp;
        currentMp = maxMp;
        SetCameraFollowPlayer();
        //stats.UpdateUIStats();
        
        Debug.Log($"current HP {currentHp}/max HP{maxHp} n/ current MP {currentMp} / Max Mp {maxMp}");
    }



    private void Update()
    {
        if(!initialCheck)
        {
            UpdateUIStats();
            initialCheck = true;
        }
    }
    public void UpdateUIStats()
    {
        MasterCanvasManager.instance.UpdateStats(
            stats.GetSwordLevel(),
            stats.GetMagicLevel(),
            currentMp,
            maxMp,
            stats.GetHPLevel(),
            currentHp,
            maxHp,
            stats.currentEXP,
            stats.expToNextLevel[stats.playerLevel]);
    }

    public void MoveToSpot(Vector3  spawnpoint)
    {
        transform.position = spawnpoint;
    }

    public void SetCameraFollowPlayer()
    {
        var vcam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        vcam.Follow = this.gameObject.transform;
    }

    public void AddSmallKeyToInventory()
    {
        inventory.AddSmallKey();
    }

}
