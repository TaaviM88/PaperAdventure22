using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class BossRoomManager : MonoBehaviour
{
    public GameObject doorRight;
    public GameObject doorLeft;
    public List<Light2D> lights2D = new List<Light2D>();
    public BossManager roomsBoss;
    public Transform bossSpawnPoint;
    public int bossRoomID = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBoss();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBoss()
    {
        GameObject boss = Instantiate(roomsBoss.gameObject, bossSpawnPoint);

        roomsBoss = boss?.GetComponent<BossManager>();
        roomsBoss.bossRoomId = bossRoomID;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameEvents.current.InitializeBossRoom(bossRoomID);
            Debug.Log("pelaaja on saapunut bossi huoneesee");
        }
    }
}
