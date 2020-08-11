using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameHandler : MonoBehaviour
{
    
    //muista lisätä yhteys pelajaan?
    [SerializeField] private GameObject unitGameObject;
    private IUnit unit;
    // Start is called before the first frame update
    void Awake()
    {
        unit = unitGameObject.GetComponent<IUnit>();
        Debug.Log(unit.GetGoldAmount());
        SaveSystem.Init();

        #region test stuff ja esimerkki
        //PlayerData playerData = new PlayerData();
        //playerData.playerName = "Link";
        //playerData.sceneName = "TestScene";
        //playerData.position = new Vector3(0, 0);
        //playerData.health = 80;

        ////JsonUtility.ToJson(playerData);
        //string json = JsonUtility.ToJson(playerData);
        //Debug.Log(json);

        //File.WriteAllText(Application.dataPath + "/SaveData /saveFile.json", json);


        // string json = File.ReadAllText(Application.dataPath + "/SaveData /saveFile.json");
        //PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);
        //Debug.Log("position: " + loadedPlayerData.position);
        // Debug.Log("health:" + loadedPlayerData.health);
        #endregion
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F4))
        {
            Save();
        }

        if(Input.GetKeyDown(KeyCode.F5))
        {
            Load();
        }
    }

    private void Save()
    {
        Vector3 playerPosition = unit.GetPosition();
        int goldAmount = unit.GetGoldAmount();

        PlayerData playerdata = new PlayerData();
        playerdata.goldAmount = goldAmount;
        playerdata.position = playerPosition;
        string json = JsonUtility.ToJson(playerdata);
        SaveSystem.Save(json);
    }

    private void Load()
    {
        string saveString = SaveSystem.Load();

        if(saveString != null)
        {
            PlayerData playerdata = JsonUtility.FromJson<PlayerData>(saveString);
            unit.SetGoldAmount(playerdata.goldAmount);
            unit.SetPosition(playerdata.position);
            Debug.Log(unit.GetGoldAmount());
        }
        else
        {
            Debug.Log("no save");
        }

    }

    private class PlayerData
    {
        public int goldAmount;
        //public string playerName;
        //public string sceneName;
        public Vector3 position;
        //public int health;
    }
}
