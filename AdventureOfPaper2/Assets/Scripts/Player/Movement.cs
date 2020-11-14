using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Collision coll;
    private BetterJumping betterjumping;
    PlayerEnumManager enums;
    private Rigidbody2D _rb2D;
    private PlayerAnimationController anime;
    PlayerManager manager;
    [Header("Parameters")]
    public float speed = 7f;
    public float jumpForce = 14f;
    public float maxVelocity = 14;
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
    [Header("Jump buffer(but counter to 0)")]
    public  int buffer_counter = 0;
    public  int buffer_max = 10;
    //Which direction player is moving
    private Vector2 moveDir;

    public int side = 1;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        _rb2D = GetComponent<Rigidbody2D>();
        betterjumping = GetComponent<BetterJumping>();
        anime = GetComponent<PlayerAnimationController>();
        enums = GetComponent<PlayerEnumManager>();
        manager = GetComponent<PlayerManager>();
        if(side == 1)
        {
            enums.SetLookDirection(PlayerLookDir.right);
        }
        else
        {
            enums.SetLookDirection(PlayerLookDir.left);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();

        if(!manager.GetCanMove())
        {
            return;
        }

        horizontalX = Input.GetAxisRaw("Horizontal");
        verticalY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(horizontalX, verticalY);
        //jump buffer
        if (!coll.onGround && _rb2D.velocity.y < 0)
        {
            if(buffer_counter < buffer_max)
            {
                buffer_counter++;
                CheckJump();
            }

        }

        if (Input.GetButtonDown("Jump"))
        {
            CheckJump();
            buffer_counter = 0;
        }

        
    }

    private void FixedUpdate()
    {
        if (manager.GetCanMove())
        {
            Move();
        }

        //clamp to max velocity

        _rb2D.velocity = new Vector2(Mathf.Clamp(_rb2D.velocity.x, -maxVelocity, maxVelocity), _rb2D.velocity.y); 

        //Debug.Log(_rb2D.velocity);
    }

    private void Move()
    {   
        if(enums.moveState == PlayerMoveState.duck)
        {
            anime.SetInputAxis(moveDir.x, moveDir.y, _rb2D.velocity.y);
            anime.SetBool("isDucking", true);
            if(moveDir.y < 0)
            {
                SetVelocityZero();
            }
            //
            return;
        }
        else
        {
            anime.SetBool("isDucking", false);
        }

       
        //jos liikutaan oikealle
        if(moveDir.x >0)
        {
            side = 1;
            //do animation flip
            anime.Flip(side);

            enums.SetLookDirection(PlayerLookDir.right);
        }
        //jos liikutaan vasemmalle
        if(moveDir.x < 0)
        {
            side = -1;
            anime.Flip(side);
            enums.SetLookDirection(PlayerLookDir.left);
        }

        //jos painetaan alaspäin niin ei voi enää hallita tippumista(aka alaspäin hyökkäys)
        //if(moveDir.y > -0.1f)
        //{
        //    //orggis
        //    
        //    //uus

        //}

        //lisää tuohon ylös mikä päätätte jotenkin niin joo näin
        _rb2D.velocity = new Vector2(moveDir.x * speed, _rb2D.velocity.y);
        //pistä animaatio kuntoon
        anime.SetInputAxis(moveDir.x, moveDir.y, _rb2D.velocity.y);
    }

    private void CheckJump()
    {
        if(!coll.onGround)
        {
            jumping = true;
            return;
        }

       DoJump();
            
    }
      
    public void DoJump()
    {
        anime.SetTrigger("Jump");
        //1.5f = kuinka paljon antaa lisää kiihtyvyyttä horisontaalisesti
        _rb2D.velocity = new Vector2(moveDir.x * speed, jumpForce);
        buffer_counter = 0;
        //_rb2D.AddForce(new Vector2(moveDir.x * speed, jumpForce), ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        if(coll.onGround)
        {
            betterjumping.enabled = true;
            jumping = false;
        }
    }

    public void SetVelocityZero()
    {
        _rb2D.velocity = new Vector2(0, _rb2D.velocity.y);
        
    }

    public void SetRigidbodyConstraits(bool freezeOn)
    {
        if(freezeOn)
        {
            _rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            _rb2D.constraints = RigidbodyConstraints2D.FreezePositionY;
            _rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            _rb2D.constraints = RigidbodyConstraints2D.None;
            _rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
