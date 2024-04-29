using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Interact_Actions : MonoBehaviour
{
    public List<string> Responses;
    
    // Start is called before the first frame update
    void Start()
    {
        #region Response List
        Responses.Add("Hello Welcome to our village");
        Responses.Add("Out with You");
        Responses.Add("My Name is Sleepy Joe");
        Responses.Add("I used to be President until... wait what was I saying");
        Responses.Add("I used to be an adventurer like you..... Until I took an arrow to the knee or Butt");
        Responses.Add("Ahhhhh you scared me");
        Responses.Add("Hey mind your buisness");
        Responses.Add("Hey yii join me for a eeink asswhile");
        Responses.Add("Bum");
        Responses.Add("Sgt Allen pooped his pants");
        Responses.Add("Sleepy Joe is stupid");
        

        #endregion
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void OnTriggerEnter2D(Collider2D other)
    // {
    //     if ()
    //     {
    //         throw new NotImplementedException();
    //     }
    // }
    //
    // public void Inrange()
    // {
    //     
    // }
}
