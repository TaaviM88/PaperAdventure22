using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Collision coll;
    private BetterJumping betterjumping;
    private Rigidbody2D _rb2D;

    [Header("Parameters")]
    public float speed = 7f;
    public float jumpForce = 14f;
    public float knockbackForceX = 15f;
    public float knockbackForceY = 15f;

    [Space]
    [Header("Parameters")]
    private bool canMove = true;
    private bool groundTouch;
    private bool jumping;
    //players joystick movement
    private float horizontalX;
    private float verticalY;
    //Which direction player is moving
    private Vector2 moveDir;

    public int side = 1;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        _rb2D = GetComponent<Rigidbody2D>();
        betterjumping = GetComponent<BetterJumping>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        horizontalX = Input.GetAxis("Horizontal");
        verticalY = Input.GetAxis("Vertical");
        moveDir = new Vector2(horizontalX, verticalY);

        if(Input.GetButtonDown("Jump"))
        {
            CheckJump(false);
        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //jos liikutaan oikealle
        if(moveDir.x >0)
        {
            side = 1;
            //do animation flip
        }
        //jos liikutaan vasemmalle
        if(moveDir.x < 0)
        {
            side = -1;
        }

        _rb2D.velocity = new Vector2(moveDir.x * speed, _rb2D.velocity.y);
        //pistä animaatio kuntoon
    }

    private void CheckJump(bool attackJump)
    {
        if(!coll.onGround)
        {
            jumping = true;
            return;
        }
        else
        {
            
            if (attackJump)
            {
                #region jump up and down attack
                if (horizontalX > 0)
                {
                    //do attackup
                }
                if(horizontalX < 0)
                {
                    //do attackJump down;
                }
                #endregion
            }

            else
            {
                //set jump animation
                DoJump();
            }
        }
       
    }

    private void DoJump()
    {
        _rb2D.velocity = new Vector2(moveDir.x * speed * 1.5f, jumpForce);
        
    }

    private void CheckGround()
    {
        if(coll.onGround)
        {
            betterjumping.enabled = true;
            jumping = false;
        }
    }
}
