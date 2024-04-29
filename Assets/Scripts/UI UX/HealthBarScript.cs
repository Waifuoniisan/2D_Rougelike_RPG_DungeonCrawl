using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class HealthBarScript : MonoBehaviour
{
    #region OG HealthBAr
    
    public Slider Slid;


    public static HealthBarScript Healthbar;
    // public Image FIll;
    public HealthBarScript healthBar;
    
    
    public void SetMaxHealth(int Health)
    {
        Slid.maxValue = Health;
        Slid.value = Health;
    }
    
    public void SetHealth(int Health)
    {
        Slid.value = Health;
    }
    
    
    
    //(Goes on playerScript)
    //Character.StatSheet.(the part before is just calling it from another script)CurrentHealth = Character.StatSheet.MaxHealth;
    //HealthBar.SetMaxHealth(Character.StatSheet.MaxHealth);
    #endregion
    // public Slider slider;
    // [SerializeField] 
    //
    //
    // public void Start()
    // {
    //     Healthbar = this;
    // }
    //
    // public void UpdateHealthBar(float CurrentValue, float MaxValue)
    // {
    //     slider.value = CurrentValue / MaxValue;
    // }
    //
    
}
