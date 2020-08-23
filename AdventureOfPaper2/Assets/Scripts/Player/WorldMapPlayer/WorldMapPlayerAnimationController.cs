using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapPlayerAnimationController : MonoBehaviour
{
    private WorldMapPlayerMovement move;
    private Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        move = GetComponent<WorldMapPlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInputAxis(float x, float y)
    {
        anime.SetFloat("MoveX", x);
        anime.SetFloat("MoveY", y);
    }

    public void SetLastMovement(float lastX, float lastY)
    {
        anime.SetFloat("LastMoveX", lastX);
        anime.SetFloat("LastMoveY", lastY);
    }

}
