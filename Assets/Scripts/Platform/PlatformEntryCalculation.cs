using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class managing setting up gate calculations and triggering recalculation of player health if player enters a gate
public class PlatformEntryCalculation : MonoBehaviour
{
    [SerializeField]
    PlatformEntryGate leftCalculation, rightCalculation;

    public void setCalculationEntryPoints(Calculation left, Calculation right)
    {
        leftCalculation.SetCalculation(left);
        rightCalculation.SetCalculation(right);
        enableGate();
    }

    public void enableGate()
    {
        leftCalculation.gameObject.SetActive(true);
        rightCalculation.gameObject.SetActive(true);
    }

    public void disableGate()
    {
        leftCalculation.gameObject.SetActive(false);
        rightCalculation.gameObject.SetActive(false);
 
    }

    public void gateEntered(bool left){
        disableGate();
        PlatformManager.Instance.OnCalculationEnter(left);
    }




    
}
