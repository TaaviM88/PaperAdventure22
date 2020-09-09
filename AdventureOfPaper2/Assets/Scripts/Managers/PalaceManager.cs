using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PalaceManager : MonoBehaviour
{
    public static PalaceManager instance;
    public GameObject[] Items;
    public Dictionary<int, bool> ItemsD =  new Dictionary<int, bool>();
    string palaceSceneName;
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
        palaceSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("palace name : " + palaceSceneName);
        CheckItems();
    }

    private void CheckItems()
    { 
        foreach (var key in ItemsD.Keys)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveItem(int id, bool collected)
    {
        ItemsD[id] = collected;
    }

    public void  AddItemToDic(int id, bool collected)
    {

    }

}
