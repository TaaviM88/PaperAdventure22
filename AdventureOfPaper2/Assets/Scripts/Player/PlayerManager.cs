using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  Cinemachine;
using System;

public class PlayerManager : MonoBehaviour, ITakeDamage<int>, IDie
{
    public static PlayerManager instance; 
    PlayerStats stats;
    PlayerInventory inventory;
    PlayerAnimationController anime;
    ////One bar of your magic/heart meter = 16 points
    public int heartContainerInHp = 16;
    public int magicBottleInHp = 16;

    public float iFrames = 0.5f;

    int currentHp;
    int maxHp;

    int currentMp;
    int maxMp;
    bool initialCheck = false;

    bool canMove = true;
    bool canAttack = true;
    bool canLift = true;
    bool isAlive = true;

    bool iframesOn = false;
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
        anime = GetComponent<PlayerAnimationController>();

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

    #region Booleannit saatana
    public void SetCanMove(bool newBoolean)
    {
        canMove = newBoolean;
    }

    public void SetCanAttack(bool newBoolean)
    {
        canAttack = newBoolean;
    }

    public void SetCanLift(bool newBoolean)
    {
        canLift = newBoolean;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public bool GetCanAttack()
    {
        return canAttack;
    }

    public bool GetCanLift()
    {
        return canLift;
    }
    #endregion

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

    public void Damage(int damage)
    {
        if(!isAlive)
        {
            return;
        }

        if(iframesOn)
        {
            return;
        }

        Debug.Log("Damage was  " + damage + " " + gameObject.name);
        currentHp = Mathf.Max(currentHp - damage, 0);
        StartCoroutine(IFrameTimer());
        anime.SetTrigger("Hurt");

        Debug.Log($"Player's current hp: {currentHp}");
        if(currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {

        anime.SetTrigger("Die");

        SetAllBooleansFalse();
        isAlive = false;

        Debug.Log("Kuolin " + gameObject.name);
    }

    IEnumerator  IFrameTimer()
    {
        iframesOn = true;
        yield return new WaitForSeconds(iFrames);
        iframesOn = false;
    }

    public void SetAllBooleansFalse()
    {
        canMove = false;
        canAttack = false;
        canLift = false;
    }

    public void SetAllBooleansTrue()
    {
        canMove = true;
        canAttack = true;
        canLift = true;
    }
}
