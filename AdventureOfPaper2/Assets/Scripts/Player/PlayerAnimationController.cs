using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    private Movement move;
    private Collision coll;

    //[HideInInspector]
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collision>();
        move = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("OnGround", coll.onGround);
    }

    public void SetInputAxis(float x, float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", yVel);
    }

    public void SetBool(string name, bool b)
    {
        anim.SetBool(name, b);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {
        bool state = (side == 1) ? false : true;
        sr.flipX = state;
    }
}
