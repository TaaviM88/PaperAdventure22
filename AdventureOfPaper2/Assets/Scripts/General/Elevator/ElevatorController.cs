using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public List<Transform> animeChilds;
    public float speed = 5f;
    public bool setPointAToStartpoint = true;
    bool playerIsOn = false;
    private float vSpeed = 0;
    [SerializeField] private Material material;
    Animator tempAnime;
    float mOffsetY = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(setPointAToStartpoint)
        {
            pointA.position = gameObject.transform.position;
        }

        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if(child.gameObject.GetComponent<Animator>())
            {
                animeChilds.Add(child);
            }
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
                vSpeed = Input.GetAxisRaw("Vertical") * speed;

                mOffsetY += vSpeed;
                material.SetVector("_Offset", new Vector4(0, mOffsetY,0,0));
             
                transform.position = new Vector3(transform.position.x, transform.position.y + vSpeed, transform.position.z);

                PlayAnimations();
            }
        }
        else
        {
            StopAnimations();
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
            StopAnimations();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //transform.DOShakePosition(0.1f, 1, 1, 0.5f, true, true).OnComplete(() => playerIsOn = true);
            // transform.DOShakeScale(0.1f, 1, 10, 0,true).OnComplete(() => playerIsOn = true);
        }
    }

    public void MoveToPoint(Transform point)
    {
        if (gameObject.transform.position != point.position)
        {
            gameObject.transform.DOMoveY(point.position.y, 0.5f, true);
        }
    }

    public void PlayAnimations()
    {
        for (int i = 0; i < animeChilds.Count; i++)
        {

            tempAnime = animeChilds[i].GetComponent<Animator>();

            //tempAnime.SetTrigger("Lift_trigger");


            tempAnime.SetBool("AnimationOn", true);
        }
     
    }

    public void StopAnimations()
    {
        for (int i = 0; i < animeChilds.Count; i++)
        {
            tempAnime = animeChilds[i].GetComponent<Animator>();

            tempAnime.SetBool("AnimationOn", false);
        }
    }
}
