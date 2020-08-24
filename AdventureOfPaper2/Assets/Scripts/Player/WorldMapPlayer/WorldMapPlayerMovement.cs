using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapPlayerMovement : MonoBehaviour
{
    public float speed = 7f;
    private WorldMapPlayerAnimationController anime;
    private Rigidbody2D _rb2D;
    private bool canMove = true;
    private float horizontalX;
    private float verticalY;
    private Vector2 moveDir;

    // Start is called before the first frame update
    void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        anime = GetComponent<WorldMapPlayerAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalX = Input.GetAxis("Horizontal");
        verticalY = Input.GetAxis("Vertical");
        moveDir = new Vector2(horizontalX, verticalY);
       
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rb2D.velocity = new Vector2(moveDir.x * speed, moveDir.y * speed);
        anime.SetInputAxis(moveDir.x, moveDir.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            anime.SetLastMovement(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
       
        }

    }
}
