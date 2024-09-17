using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    
    #region Stats
    
    [SerializeField] 
     int 
        CurrentHealth, 
        MaxHealth, 
        CurrentExprience, 
        MaxExprience, 
        CurrentLevel;
     private float
         CurrentHeal;
   public static int
     DamagePower = 1;
        
   
    #endregion

    #region Healthabr ref

    [SerializeField]
    public HealthBarScript Health;
    public static Character StatSheet;
    public ExpBarScript Exper;
    #endregion

    public GameObject Player;
    public SpriteRenderer SR;
    public TextMeshProUGUI Levelnumb;
    public TextMeshProUGUI progressMaxTXT;
    public TextMeshProUGUI progressCurrentTXT;
    public TextMeshProUGUI HPMaxTXT;
    public TextMeshProUGUI HPCurrentTXT;
   // public static Character StatSheet;
    // private void Awake()
    // {
    //    Health = GetComponentInChildren<HealthBarScript>();
    // }

    private void Start()
    {
        Character.StatSheet = this;
        DontDestroyOnLoad(this);
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
        SR = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        CurrentHealth = MaxHealth;
        Health.SetMaxHealth(MaxHealth);
        Exper.SetMaxExp(MaxExprience);
        // Health.UpdateHealthBar(CurrentHealth,MaxHealth);
    }

    //Enable is a lifecycle to call the system to level up (basically using it to call the exp system code)
   //Note** Dont use () on the HandleExpChange so you dont invoke it
   //Subs to the event
   //Note** might be better to use the for calling events from now on(practice more)
    private void OnEnable()
    {
        ExpManager.Instance.OnExpChange += HandleExpChange;
    }

    //Use the disable function to stop it as said
    //Unsubs so the event is getting called anymore
    private void OnDisable()
    {
        ExpManager.Instance.OnExpChange -= HandleExpChange;
    }

     void HandleExpChange(int NewExprience)
    {
        Exper.SetExp(CurrentExprience);
        CurrentExprience += NewExprience;
        if (CurrentExprience >= MaxExprience)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        MaxHealth += 10;
        CurrentHealth = MaxHealth;
        DamagePower++;

        CurrentLevel++;

        CurrentExprience = 0;
        MaxExprience += 100;
    }

    
    
    public void TakeDamage(int DamageAmount)
    {
        //Health.UpdateHealthBar(CurrentHealth,MaxHealth);
        CurrentHealth -= DamageAmount;
        Health.SetHealth(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            LoadScene();
        }
    }

    public void Update()
    {
        Levelnumb.SetText(CurrentLevel.ToString());
        progressMaxTXT.SetText(MaxExprience .ToString());
        progressCurrentTXT.SetText(CurrentExprience.ToString());
        HPMaxTXT.SetText(MaxHealth.ToString());
        HPCurrentTXT.SetText(CurrentHealth.ToString());
    }

    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver 1");
        Destroy(Player);
    }
}
