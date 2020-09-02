using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public GameObject[] Spawnpoints;
    StartPoint spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnpoint = GameManager.instance.GetSpawnpoint();
        SetPlayerSpawnpoint();    
    }

    private void SetPlayerSpawnpoint()
    {
        switch (spawnpoint)
        {
            case StartPoint.Apoint:
                for (int i = 0; i < Spawnpoints.Length; i++)
                {
                    if(Spawnpoints[i].gameObject.name == "Apoint")
                    {
                        PlayerManager.instance.MoveToSpot(Spawnpoints[i].transform.position);
                        Debug.Log("No niin A");
                    }
                }
                break;

            case StartPoint.Bpoint:
                for (int i = 0; i < Spawnpoints.Length; i++)
                {
                    if (Spawnpoints[i].gameObject.name == "Bpoint")
                    {
                        PlayerManager.instance.MoveToSpot(Spawnpoints[i].transform.position);
                        Debug.Log("No niin B");
                    }
                }
                break;

            case StartPoint.Cpoint:
                for (int i = 0; i < Spawnpoints.Length; i++)
                {
                    if (Spawnpoints[i].gameObject.name == "Cpoint")
                    {
                        PlayerManager.instance.MoveToSpot(Spawnpoints[i].transform.position);
                        Debug.Log("No niin C");
                    }
                }
                break;

            case StartPoint.Dpoint:
                for (int i = 0; i < Spawnpoints.Length; i++)
                {
                    if (Spawnpoints[i].gameObject.name == "Dpoint")
                    {
                        PlayerManager.instance.MoveToSpot(Spawnpoints[i].transform.position);
                        Debug.Log("No niin D");
                    }
                }
                break;

            default:
                if(Spawnpoints.Length != 0)
                {

                    PlayerManager.instance.MoveToSpot(Spawnpoints[0].transform.position);
                }
                else
                {
                    PlayerManager.instance.MoveToSpot(Vector3.zero);
                }

                Debug.Log("No niin Default");
                break;
        }
        //GameManager.instance.
    }
}
