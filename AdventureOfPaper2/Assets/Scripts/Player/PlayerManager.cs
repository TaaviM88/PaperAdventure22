using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IUnit
{

    int goldAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.End))
        {
            SetGoldAmount(goldAmount + 1);
            Debug.Log("Current Gold Amount: " + GetGoldAmount());
        }
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetGoldAmount(int goldAmount)
    {
       this.goldAmount = goldAmount;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
