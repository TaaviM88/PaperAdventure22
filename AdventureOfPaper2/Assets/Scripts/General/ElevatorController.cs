using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 5f;
    public bool setPointAToStartpoint = true;
    bool playerIsOn = false;
    private float vSpeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(setPointAToStartpoint)
        {
            pointA.position = gameObject.transform.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsOn)
        {
            Move();
        }
    }

    private void Move()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if(transform.position.y <= pointA.position.y && transform.position.y >= pointB.position.y)
            {
                vSpeed = Input.GetAxis("Vertical") * speed;
                transform.position = new Vector3(transform.position.x, transform.position.y + vSpeed, transform.position.z);
            }
        }
        #region Elevator position check
        if(transform.position.y < pointB.position.y)
        {
           transform.position = pointB.position; 
        }

        if(transform.position.y > pointA.position.y)
        {
            transform.position = pointA.position; 
        }
        #endregion
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerIsOn = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
         if(other.tag == "Player")
        {
            playerIsOn = false;
        }
    }
}
