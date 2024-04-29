using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //StaticVars
    public static PlayerScript Player;

    public static GameManager Manager;
    //Scripts
    public HealthBarScript HealthBar;
    
    //Int
    // public int maxHealth = 10;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager = this;
        
        // PlayerScript.player.currenthealth = PlayerScript.player.maxHealth;
        // HealthBar.SetMaxHealth(PlayerScript.player.maxHealth);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // public void TakeDamage(int DamageAmount)
    // {
    //     PlayerScript.player.currenthealth -= DamageAmount;
    //     
    //     if (PlayerScript.player.currenthealth <= 0)
    //     {
    //         Destroy(PlayerScript.player);
    //     }
    // }
   

}
