using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    bool enableGate = true;
    [SerializeField]
    Calculation leftCalculation, rightCalculation;

    PlatformEntryCalculation platformEntryCalculation;

    private void Awake() {
        platformEntryCalculation = GetComponentInChildren<PlatformEntryCalculation>();
        if(enableGate){
            platformEntryCalculation.setCalculationEntryPoints(leftCalculation, rightCalculation);
        }else{
            platformEntryCalculation.disableCalculation();
        }
    }


}
