﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimeManager))]
public class EnemyManager : MonoBehaviour, ITakeDamage<int>, IDie
{
    public int health = 2;
    public int side = -1;
    public int exp = 2;
    public ExpToNumberTextMesh expNumber;
    private EnemyAnimeManager anime;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<EnemyAnimeManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Damage(int damage)
    {
        Debug.Log("Damage was  " + damage + " " + gameObject.name);
        health = Mathf.Min(health - damage, 0);
        anime.SetTrigger("Damaged");

        if(health <= 0)
        {
            Die();
        }
    }



    public void Die()
    {
        Debug.Log("kuolin t." + gameObject.name);
        //tee kuolin animaation ja anna expat
        anime.SetTrigger("Death");
        SpawnExpNumber();

    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void SpawnExpNumber()
    {
        //Vector3 pos;
        //float x = Camera.main.ViewportToScreenPoint(transform.position).x;
        //float y = Camera.main.ViewportToScreenPoint(transform.position).y;
        //pos = new Vector3(x, y, 0f); 
        Instantiate(expNumber, transform.position, Quaternion.identity).SetExp(exp);
    }

    public void FlipEnemy()
    {
        Vector3 flipped = transform.localScale;
        flipped.x *= -1;
        transform.localScale = flipped;
        side = (int)flipped.x;
    }
}
