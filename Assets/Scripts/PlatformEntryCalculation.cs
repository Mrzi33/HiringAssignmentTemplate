using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEntryCalculation : MonoBehaviour
{
    [SerializeField]
    PlatformEntryGate leftCalculation, rightCalculation;

    public void setCalculationEntryPoints(Calculation left, Calculation right)
    {
        leftCalculation.SetCalculation(left);
        rightCalculation.SetCalculation(right);
    }

    public void disableCalculation()
    {
        leftCalculation.gameObject.SetActive(false);
        rightCalculation.gameObject.SetActive(false);
 
    }

    public void gateEntered(bool left){
        disableCalculation();
    }




    
}
