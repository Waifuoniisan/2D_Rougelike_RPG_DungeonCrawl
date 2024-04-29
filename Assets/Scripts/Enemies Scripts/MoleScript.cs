using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleScript : MonoBehaviour
{
    public static MoleScript Teddy;
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
    public float lerpSpeed = 5f;
    public float chaseCooldown = 1f;
    private float cooldownTimer = 0f;
    //Ints
    // public int DamageAmount = 5;
    public int maxHealth = 5;
    private int currentHealth;
    public int Expamount = 100;

    public Direction MyDirection;
    public PawnSensing MyAction;

    public Animator MoleAnim;

    public Rigidbody2D MoleRB;

    public Transform player;
    private bool isChasing = false;
    
    public PolygonCollider2D poly;
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Teddy = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        poly = GameObject.FindGameObjectWithTag("Enemies").GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 move = MoleRB.velocity;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
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

        #endregion

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                cooldownTimer = 0f;
            }
        }

        PawnSensing a = PawnSensing.Idle;
        //MoleRB.velocity = move;
    }
    void ChasePlayer()
    {
        if (cooldownTimer > 0f)
            return;
        //Calls to get the players position and help get the direction this works better then trying to use the system I used in the player under the movement code
        //Player is a var that is called as a Gameobject(Goal will be never having to put the object in by hand)
        //Chargedspeed is a float to boosat a speed and use melee attack (needs a update to the animation for that)
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        MoleRB.velocity = directionToPlayer * chargedspeed;
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
            MoleAnim.Play("Mole_Idle");
        }
        else if (MyAction == PawnSensing.Walking)
        {
            MoleAnim.Play(WalkAnimation());
        }
        else if (MyAction == PawnSensing.Chase)
        {
            MoleAnim.Play(ChaseAnimation());
        }
        else if (MyAction == PawnSensing.Death)
        {
            MoleAnim.Play("On_DeathMole");
        }

    }

    #endregion

    string WalkAnimation()
    {
        string AnimationPrefix = "Mole_Walk_";

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
            Character.StatSheet.TakeDamage(TreantScript.DamageAmount);
        }
    }
    public void MoleDamage(int damageAmount)
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
        float deathAnimationDuration = MoleAnim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(deathAnimationDuration);
        
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Arrow"))
        {
            MoleDamage(1);
        }
    }
}
