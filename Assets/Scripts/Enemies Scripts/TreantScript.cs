using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreantScript : MonoBehaviour
{
    public static TreantScript Treant;

    #region Enums

    public enum PawnSensing
    {
        Idle,
        Walking,
        Chase,
        Death
    }

    public enum Direction
    {
        Front,
        Back,
        Left,
        Right
    }

    #endregion

    #region Vars

    //Speed
    public float attackspeed = 5;
    public float movespeed = 5;
    public float chargedspeed = 10;
    public float AgroRange = 3;
    
    //Ints
    public static int DamageAmount = 5;
    public int maxHealth = 5;
    private int currentHealth;
    private int Expamount = 100;
    public State state;

    public Direction MyDirection;
    public PawnSensing MyAction;

    public Animator TreantAnim;

    public Rigidbody2D TreantRB;

    public Transform player;

    
    public float lerpSpeed = 5f;
    public float chaseCooldown = 1f;
    private float cooldownTimer = 0f;
    private bool isChasing = false;
    
    public PolygonCollider2D poly;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        Treant = this;
        // Character.StatSheet.
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 move = TreantRB.velocity;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        //The move code was better then the reg move code this helps the enemy move in a better, and the RB get called to move at a chargespeed.
        //But first add the Var for how fast you move.
        #region EnemyMovement Code

        if (distanceToPlayer < AgroRange)
        {
            if (!isChasing)
            {
                SetAction(PawnSensing.Chase);
                isChasing = true;
            }

            ChasePlayer();
        }
        else
        {
            SetAction(PawnSensing.Idle);
            isChasing = false;
        }
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                cooldownTimer = 0f;
            }
        }
        #endregion

        PawnSensing a = PawnSensing.Idle;
        //TreantRB.velocity = move;
    }


    void ChasePlayer()
    {
        if (cooldownTimer > 0f)
            return;
        //Calls to get the players position and help get the direction this works better then trying to use the system I used in the player under the movement code
        //Player is a var that is called as a Gameobject(Goal will be never having to put the object in by hand)
        //Chargedspeed is a float to boosat a speed and use melee attack (needs a update to the animation for that)
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        TreantRB.velocity = directionToPlayer * chargedspeed;
        transform.position = Vector2.Lerp(transform.position, player.position, lerpSpeed * Time.deltaTime);
        
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;


        if (directionToPlayer.y > 0)
        {
            SetDirection(Direction.Front);
        }
        else
        {
            SetDirection(Direction.Back);
        }

        if (directionToPlayer.x > 0)
        {
            SetDirection(Direction.Right);
        }
        else
        {
            SetDirection(Direction.Left);
        }
        if (Vector2.Distance(transform.position, player.position) < 0.1f)
        {
            cooldownTimer = chaseCooldown;
        }
    }


    #region Animation Update by direction

    public void SetDirection(Direction d)
    {
        if (d == MyDirection)
        {
            return;
        }

        MyDirection = d;
        UpdateAnim();
    }

    public void SetAction(PawnSensing a)
    {
        if (a == MyAction)
        {
            return;
        }

        MyAction = a;
        UpdateAnim();
    }

    public void UpdateAnim()
    {
        if (MyAction == PawnSensing.Idle)
        {
            TreantAnim.Play("Treant_Idle");
        }
        else if (MyAction == PawnSensing.Walking)
        {
            TreantAnim.Play(WalkAnimation());
        }
        else if (MyAction == PawnSensing.Chase)
        {
            TreantAnim.Play(ChaseAnimation());
        }
        else if (MyAction == PawnSensing.Death)
        {
            TreantAnim.Play("On_DeathTreant");
        }

    }

    #endregion

    string WalkAnimation()
    {
        string AnimationPrefix = "Treant_Walk_";

        switch (MyDirection)
        {
            case Direction.Front:
                return AnimationPrefix + "Front";
            case Direction.Back:
                return AnimationPrefix + "Back";
            case Direction.Left:
                return AnimationPrefix + "Left";
            case Direction.Right:
                return AnimationPrefix + "Right";
            default:
                return AnimationPrefix + "Front";
        }
    }

    string ChaseAnimation()
    {
        string AnimationPrefix = "Treant_Walk_";

        switch (MyDirection)
        {
            case Direction.Front:
                return AnimationPrefix + "Front";
            case Direction.Back:
                return AnimationPrefix + "Back";
            case Direction.Left:
                return AnimationPrefix + "Left";
            case Direction.Right:
                return AnimationPrefix + "Right";
            default:
                return AnimationPrefix + "Front";
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DamagePlayer(PlayerScript.player.gameObject);
        }
    }

    public void DamagePlayer(GameObject player)
    {
        PlayerScript playerHealth = player.GetComponent<PlayerScript>();

        if (playerHealth != null)
        {
            Character.StatSheet.TakeDamage(DamageAmount);
        }
    }
    public void EnemyDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
            Destroy(poly);
        }
    }

    void Die()
    {
        ExpManager.Instance.AddExp(Expamount);
        SetAction(PawnSensing.Death);
        UpdateAnim();
        StartCoroutine(AnimationFirst());
    }

    IEnumerator AnimationFirst()
    {
        //PLACEHOLDER=== this replaces the round about way of using the invoke fuction as the issues are with a werid difference in time.Doesnt look nice otherwise when the event happens
        float deathAnimationDuration = TreantAnim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(deathAnimationDuration);
        
        Destroy(gameObject);
    }
    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Arrow"))
        {
            EnemyDamage(Character.DamagePower);
        }
    }
    
}