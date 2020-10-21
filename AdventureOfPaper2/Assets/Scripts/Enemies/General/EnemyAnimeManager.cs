using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimeManager : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFloat(string name, float value)
    {
        anim.SetFloat(name, value);
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
        spriteRenderer.flipX = state;
    }
}
