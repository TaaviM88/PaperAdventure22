using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PlayerData playerData = new PlayerData();
        //playerData.playerName = "Link";
        //playerData.sceneName = "TestScene";
        //playerData.position = new Vector3(0, 0);
        //playerData.health = 80;

        ////JsonUtility.ToJson(playerData);
        //string json = JsonUtility.ToJson(playerData);
        //Debug.Log(json);

        //File.WriteAllText(Application.dataPath + "/SaveData /saveFile.json", json);

        string json = File.ReadAllText(Application.dataPath + "/SaveData /saveFile.json");
       PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);
       Debug.Log("position: " + loadedPlayerData.position);
        Debug.Log("health:" + loadedPlayerData.health);
    }

    private class PlayerData
    {
        public string playerName;
        public string sceneName;
        public Vector3 position;
        public int health;
    }
}
