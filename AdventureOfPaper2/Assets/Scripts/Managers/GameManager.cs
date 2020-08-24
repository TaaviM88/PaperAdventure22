using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    StartPoint startpoint;
    string lastSceneName;
    string sceneName;
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
    }

    public void LoadInsideScene(string levelName, StartPoint spawnpoint)
    {
        startpoint = spawnpoint;
        SceneManager.LoadScene(levelName);
        MasterCanvasManager.instance.ToggleStatsUI(true);
        MasterCanvasManager.instance.SetRenderCamera();
    }

    public void LoadWorldMap(StartPoint spawnPoint)
    {
        lastSceneName = SceneManager.GetActiveScene().name;
        startpoint = spawnPoint;
        SceneManager.LoadScene("WorldMap_Retro");
        MasterCanvasManager.instance.ToggleStatsUI(false);
        Debug.Log(lastSceneName);
    }

    public string GetLastSceneName()
    {
        return lastSceneName;
    }

    public StartPoint GetSpawnpoint()
    {
        return startpoint;
    }
}
