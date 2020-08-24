using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapPlayerManager : MonoBehaviour
{
    public static WorldMapPlayerManager instance;
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

    public void MoveToSpot(Vector3 spawnpoint)
    {
        transform.position = spawnpoint;
    }
}
