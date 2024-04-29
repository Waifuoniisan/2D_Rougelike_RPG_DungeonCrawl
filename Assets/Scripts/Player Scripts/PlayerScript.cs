using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
  
    #region Public Var

    //Rigidbody
    public Rigidbody2D PlayerRB;
   
    //Int
   
    
    //Animator
    public Animator PlayerAnim;
    
    //Floats
    public float speed = 5f;
    
    //TimeSlates
    
    
    //Transform
    public Transform ProjectileSpawnPoint;
    
    
    //Gameobject
    public GameObject Arrow;
    
    //StaticVars
    public static PlayerScript player;
    
    //Varibles Named
    public Direction MyDirection;
    public CharacterAction MyAction;
    
    public HealthBarScript HealthBar;
    
    public AudioSource As;

    public AudioClip Attack;

    public static Canvas GPUI;

    public static TextMeshProUGUI Cointxt;
    public static TextMeshProUGUI Gemtxt;
    #endregion


    #region Public Enums

    public enum Direction{
        Front,
        Right,
        Back,
        Left
    }

    public enum CharacterAction{
        Idle,
        Walking,
        Attacking,
    }

   
    #endregion
 
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerScript.player = this; 
        DontDestroyOnLoad(this);
        Cointxt = GameObject.FindGameObjectWithTag("GolfCoin").GetComponent<TextMeshProUGUI>();
        Cointxt.text = PlayerPrefs.GetInt("Currency").ToString();
        Gemtxt = GameObject.FindGameObjectWithTag("GemCoin").GetComponent<TextMeshProUGUI>();
        Gemtxt.text = PlayerPrefs.GetInt("Currency").ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = PlayerRB.velocity;
        CharacterAction a = CharacterAction.Idle;
        
        #region Movement Code

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x = -speed;
            SetDirection(Direction.Left);
            a = CharacterAction.Walking;

        }
        

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move.x = speed;
            SetDirection(Direction.Right);
            a = CharacterAction.Walking;
        }
        else
        {
            move.x = 0;
        }
        

        if (Input.GetKey(KeyCode.UpArrow))
        {
            move.y = speed;
            SetDirection(Direction.Back);
            a = CharacterAction.Walking;
        }
        

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            move.y = -speed;
            SetDirection(Direction.Front);
            a = CharacterAction.Walking;
        }
        else
        {
            move.y = 0;
        }

        #endregion
        #region Attack Code

        if (Input.GetKeyDown(KeyCode.Z))
        {
            a = CharacterAction.Attacking;

            if (MyDirection == Direction.Left)
            {
                Instantiate(Arrow, ProjectileSpawnPoint.position, Quaternion.Euler(0, 0, 90));
            }
            
            if (MyDirection == Direction.Right)
            {
                Instantiate(Arrow, ProjectileSpawnPoint.position, Quaternion.Euler(0,0,270));
            }
            
            if (MyDirection == Direction.Back)
            {
                Instantiate(Arrow, ProjectileSpawnPoint.position, Quaternion.Euler(0,0,0));
            }
            if (MyDirection == Direction.Front)
            {
                Instantiate(Arrow, ProjectileSpawnPoint.position, Quaternion.Euler(0,0,180));
            }
           
            
        }

        #endregion

        if (Input.GetKeyDown(KeyCode.F))
        { 
            Character.StatSheet.TakeDamage(1);
            PlayerAnim.Play("Player_Hit");
        }
        SetAction(a);
        // PlayerRB.velocity = move;
        PlayerRB.velocity = Vector2.Lerp(PlayerRB.velocity, move, Time.deltaTime * 7);

    }

    public void SetDirection(Direction d)
    {
        if (d == MyDirection)
        {
            return;
        }
        MyDirection = d;
        UpdateAnim();
    }

    public void SetAction(CharacterAction a)
    {
        if (a == MyAction)
        {
            return;
        }
        MyAction = a;
        UpdateAnim();
    }

    #region Animation Update by direction

    public void UpdateAnim()
    {
        if (MyDirection == Direction.Back)
        {
            if (MyAction == CharacterAction.Idle)
            {
                PlayerAnim.Play("Player_IdleBack");
            }

            if (MyAction == CharacterAction.Walking)
            {
                PlayerAnim.Play("Player_WalkBack");
            }

            if (MyAction == CharacterAction.Attacking)
            {
                PlayerAnim.Play("Player_attackback");
            }
        }
        if (MyDirection == Direction.Front)
        {
            if (MyAction == CharacterAction.Idle)
            {
                PlayerAnim.Play("Player_IdleFront");
            }

            if (MyAction == CharacterAction.Walking)
            {
                PlayerAnim.Play("Player_WalkFront");
            }

            if (MyAction == CharacterAction.Attacking)
            {
                PlayerAnim.Play("Player_attackfront");
            }
        }

        if (MyDirection == Direction.Right)
        {
            if (MyAction == CharacterAction.Idle)
            {
                PlayerAnim.Play("Player_IdleSideRight");
            }

            if (MyAction == CharacterAction.Walking)
            {
                PlayerAnim.Play("Player_WalkSide");
            }

            if (MyAction == CharacterAction.Attacking)
            {
                PlayerAnim.Play("Player_attackside");
            }
        }
        if (MyDirection == Direction.Left)
        {
            if (MyAction == CharacterAction.Idle)
            {
                PlayerAnim.Play("Player_IdleSideLeft");
            }

            if (MyAction == CharacterAction.Walking)
            {
                PlayerAnim.Play("Player_WalkSideLeft");
            }

            if (MyAction == CharacterAction.Attacking)
            {
                PlayerAnim.Play("Player_attacksideleft");
            }
        }
        
    }

    #endregion


    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            PlayerAnim.Play("Player_Hit");
            Character.StatSheet.TakeDamage(TreantScript.DamageAmount);
        }
        
    }
}