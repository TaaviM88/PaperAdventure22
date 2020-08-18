using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpToNumber : MonoBehaviour
{
    public TMP_Text expText;
    public float lifeTime = 1f;
    public float moveSpeed = 1f;

    public float placementJitter = 0.5f;
    PlayerStats player;
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
        transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
    }

    public void SetExp(int expAmount)
    {
        expText.text = expAmount.ToString();
        transform.position += new Vector3(Random.Range(-placementJitter, placementJitter), Random.Range(-placementJitter, placementJitter), 0);
        player = FindObjectOfType<PlayerStats>();

        if(player == null)
        {
            Debug.LogError("Player Not found");
        }
        else
        player.AddExp(expAmount);
    }
}
