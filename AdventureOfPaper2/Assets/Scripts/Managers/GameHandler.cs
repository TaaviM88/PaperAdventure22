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
    [SerializeField] public PalaceManager palaceManager;
    // Start is called before the first frame update
    void Awake()
    {
        unit = unitGameObject.GetComponent<IUnit>();
        Debug.Log(unit.GetLevel());
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
        #region Levels:
        int currentLevel = unit.GetLevel();
        int swordLevel = unit.GetSwordLevel();
        int magicLevel = unit.GetMagicLevel();
        int hpLevel = unit.GetHPLevel();
        #endregion

        #region Containers:
        int heartContainer = unit.GetHeartContainerAmount();
        int magicBottle = unit.GetMagicBottleAmount();
        #endregion

        #region Progress:
        Vector3 playerPosition = unit.GetPosition();
        string palaceName = palaceManager?.GetPalaceName();
        List<GameObject> itemList = palaceManager.GetItemList();
        #endregion

        SaveData playerdata = new SaveData();

        //Levels:
        playerdata.level = currentLevel;
        playerdata.swordLevel = swordLevel;
        playerdata.magicLevel = magicLevel;
        playerdata.hpLevel = hpLevel;
        //Containers:
        playerdata.heartContainer = heartContainer;
        playerdata.magicBottle = magicBottle;
        //Progress:
        playerdata.playerPosition = playerPosition;
        playerdata.palaceName = palaceName;

        //json matskut
        string json = JsonUtility.ToJson(playerdata);
        SaveSystem.Save(json);
    }

    private void Load()
    {
        string saveString = SaveSystem.Load();

        if(saveString != null)
        {
            SaveData playerdata = JsonUtility.FromJson<SaveData>(saveString);
            #region Levels:
            unit.SetLevel(playerdata.level);
            unit.SetSwordlLevel(playerdata.swordLevel);
            unit.SetMagicLevel(playerdata.magicBottle);
            unit.SetHPLevel(playerdata.hpLevel);
            #endregion

            #region Containers:
            unit.SetHeartContainerAmount(playerdata.heartContainer);
            unit.SetMagicBottleAmount(playerdata.magicBottle);
            #endregion

            #region Progress:
            unit.SetPosition(playerdata.playerPosition);
            #endregion
            Debug.Log(unit.GetLevel());
        }
        else
        {
            Debug.Log("no save");
        }

    }

    private void LoadPalaceItemList()
    {

    }


    private class SaveData
    {
        public Vector3 playerPosition;

        #region Levels
        public int level;
        public int swordLevel;
        public int hpLevel;
        public int magicLevel;
        #endregion

        #region Containers
        public int heartContainer;
        public int magicBottle;
        #endregion

        #region InventoryList
        //itemeitä mitkä on saatu
        #endregion

        #region Progress
        //pelaajan nimi
        public string playerName;
        //scene mihin jäätiin
        public string sceneName;
        //voitetut temppelit
        public int beatenPalaceAmount;
        //avainten määrä
        public int smallKeyAmount;
        #endregion


        #region Palace
        public string palaceName;

        #endregion

        //public int health;
    }

    private class ParapaPalaceItemList
    {
        public List<GameObject> parapaPalaceItemList;
    }
}
