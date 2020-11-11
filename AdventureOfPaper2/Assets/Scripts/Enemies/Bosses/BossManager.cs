using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour, ITakeDamage<int>, IDie
{
    public int bossRoomId { get; set; }
    public int health;
    [SerializeField]
    protected int currentHp;
    public bool isAlive;
    public bool iframesOn = false;
    public float iFrames = 0.1f;
    protected EnemyAnimeManager anime;
    
    [SerializeField]
    protected List<Collider2D> colliders = new List<Collider2D>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anime = GetComponent<EnemyAnimeManager>();
        currentHp = health;
        GameEvents.current.onInitializeBossRoom += onInitializeBoss;
        EnableAllCollisions();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
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

        Debug.Log($"Player's current hp: {currentHp}");
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
