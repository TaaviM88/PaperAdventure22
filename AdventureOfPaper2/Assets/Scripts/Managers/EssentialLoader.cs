using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameManager;
    public GameObject audioManager;
    public GameObject eventSystem;
    //save matskut


    // Start is called before the first frame update
    void Awake()
    {
        InstantiateUICanvas();
        InstantiatePlayer();
        InstantiateGameManager();
        InstantiateAudioManager();
        InstantiateEventSystem();
        InstantiateSaveManager();
    }

    private void InstantiateSaveManager()
    {
        throw new NotImplementedException();
    }

    private void InstantiateEventSystem()
    {
        throw new NotImplementedException();
    }

    private void InstantiateAudioManager()
    {
        throw new NotImplementedException();
    }

    private void InstantiateGameManager()
    {
        throw new NotImplementedException();
    }

    private void InstantiatePlayer()
    {
        throw new NotImplementedException();
    }

    private void InstantiateUICanvas()
    {
        throw new NotImplementedException();
    }

}
