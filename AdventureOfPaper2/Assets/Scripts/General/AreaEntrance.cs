using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string[] areaToLoad;

    public StartPoint starpoint = StartPoint.Apoint;
    AreaEntrance entrance;
    public float waitToLoad = 0.1f;
    private bool shouldLoadAfter;
    bool playerStartHere = false;
    // Start is called before the first frame update

    private void Start()
    {
        for (int i = 0; i < areaToLoad.Length; i++)
        {
            if (areaToLoad[i] == GameManager.instance.GetLastSceneName() && starpoint == GameManager.instance.GetSpawnpoint())
            {
                playerStartHere = true;
                //PlayerManager.instance.MoveToSpot(transform.position);
                WorldMapPlayerManager.instance.MoveToSpot(transform.position);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !playerStartHere)
        {
            GameManager.instance.LoadInsideScene(areaToLoad[0], starpoint);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag =="Player" && playerStartHere)
        {
            playerStartHere = false;
        }
    }

}
