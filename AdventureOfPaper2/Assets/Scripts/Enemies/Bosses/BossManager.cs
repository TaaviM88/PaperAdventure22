using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour, ITakeDamage<int>, IDie
{
    public int bossRoomId { get; set; }
    public int health;
    public int side = -1;
    [SerializeField]
    protected int currentHp;
    public bool isAlive;
    public bool iframesOn = false;
    public float iFrames = 0.1f;
    public float actionCooldownInSeconds = 1;
    protected float currentCooldown;
    public bool randomizeActionCooldown = true;
    protected EnemyAnimeManager anime;
    public LayerMask PlayerLayer;
    [SerializeField]
    public BossState state { get; set; }
    [SerializeField]
    protected List<Collider2D> colliders = new List<Collider2D>();

    protected bool canDoAction = false;
    protected bool actioncooldownIsOn = false;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        anime = GetComponent<EnemyAnimeManager>();
        currentHp = health;
        GameEvents.current.onInitializeBossRoom += onInitializeBoss;
        EnableAllCollisions();
        anime.SetBool("IsAlive", isAlive);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        switch (state)
        {
            case BossState.Spawning:
                break;
            case BossState.Idling:
                
                if(canDoAction)
                {
                    if (!DoRaycast())
                    {
                        FlipBoss();
                    }
                    else
                    {
                        DoAction();
                    }
                }
              
                else
                {
                    if (!actioncooldownIsOn)
                    {
                        StartCoroutine(ActionCooldown());
                    }
                    
                }
              
                break;
            case BossState.Attacking:

                break;
            case BossState.TakingDamage:
                break;
            case BossState.Dying:
                break;
            case BossState.Dead:
                break;
            default:
                break;
        }
    }

    protected virtual void FlipBoss()
    {
        Debug.Log("Käännyn");
        if(side == 1)
        { 
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            side = -1;
        

        }
        else
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            side = 1;
        }

        canDoAction = false;
    }

    protected virtual bool DoRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.right * side, 10, PlayerLayer);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual float CheckPlayerDistance()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.right * side, 10, PlayerLayer);
        if (hit.collider != null)
        {
            Debug.Log("Etäisyys: " + hit.distance);

            return hit.distance;
        }

        return 0;
    }

    protected virtual void DoAction()
    {
        //DO Stuff
    //    float playerDistance = DoRaycast();

    //    if(playerDistance <= 1 && playerDistance > 0)
    //    {
    //        //Do Close Melee attack
    //    }

    //    if(playerDistance <= 2 && playerDistance > 1)
    //    {
    //        //Do FlameThrower
    //    }

    //    if(playerDistance > 2)
    //    {
    //        //Do Roll attack
    //    }
        
    }

   public virtual IEnumerator ActionCooldown()
    {
        actioncooldownIsOn = true;
        canDoAction = false;
        if (randomizeActionCooldown)
        {
            currentCooldown = UnityEngine.Random.Range(actionCooldownInSeconds * 0.5f, actionCooldownInSeconds * 1.5f);
          
        }else
        {
            currentCooldown = actionCooldownInSeconds;
        }
      
        yield return new WaitForSeconds(currentCooldown);
        actioncooldownIsOn = false;
        canDoAction = true;
    }

    public virtual void Damage(int damage)
    {
        if (!isAlive)
        {
            return;
        }

        if (iframesOn)
        {
            return;
        }

        Debug.Log("Damage was  " + damage + " " + gameObject.name);
        currentHp = Mathf.Max(currentHp - damage, 0);
        
        StartCoroutine(IFrameTimer());

        //Debug.Log($"Player's current hp: {currentHp}");
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //Kuoleminen
        Debug.Log("Voi ei se olenkin minä joka kuolen!");
    }



    IEnumerator IFrameTimer()
    {
        iframesOn = true;
        yield return new WaitForSeconds(iFrames);
        iframesOn = false;
    }

    protected virtual void onInitializeBoss(int id)
    {
      
    }

    protected virtual void EnableAllCollisions()
    {
        var allColliders = GetComponents<Collider2D>();

        for (int i = 0; i < allColliders.Length; i++)
        {
            allColliders[i].enabled = true;
        }
    }

    protected virtual void DisableAllCollisions()
    {
        var allColliders = GetComponents<Collider2D>();

        for (int i = 0; i < allColliders.Length; i++)
        {
            allColliders[i].enabled = false;
        }
    }

    protected void OnDestroy()
    {
        GameEvents.current.onInitializeBossRoom -= onInitializeBoss;
    }

    protected void OnDisable()
    {
        GameEvents.current.onInitializeBossRoom -= onInitializeBoss;
    }
}
