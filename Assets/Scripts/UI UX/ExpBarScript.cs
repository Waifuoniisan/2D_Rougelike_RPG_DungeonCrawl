using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;
public class ExpBarScript : MonoBehaviour
{
    public Slider Slid;
    
    
    public void SetMaxExp(int Exp)
    {
        Slid.maxValue = Exp;
        Slid.value = Exp;
    }
    
    public void SetExp(int Exp)
    {
        Slid.value = Exp;
    }
   
}
