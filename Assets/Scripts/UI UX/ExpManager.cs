using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code will allow me to give different creatures, enemies, etc. exp amounts
//**no scaling as of yet just flat amounts
public class ExpManager : MonoBehaviour
{
    //Professor Note **** YT video Code BmO 
    public static ExpManager Instance;

   // using delegate as a return type and used as a function
   public delegate void ExpChangeHandler(int amount);

   public event ExpChangeHandler OnExpChange;
    //This is to make sure their is only one of this at anytime.
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
//we call the delegate as an event, and us that to invoke as a function
//to call in the character code using this as a game manager
    public void AddExp(int amount)
    {
        OnExpChange.Invoke(amount);
    }
}
