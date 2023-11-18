using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// class to cummunicate between the platform parts and the platform manager
public class Platform : MonoBehaviour
{
    [SerializeField]

    PlatformEntryCalculation platformEntryCalculation;
    [SerializeField]
    EnemyGroupManager enemyGroupManager;
    [SerializeField]
    Transform starPosition, endPosition;



    public void SetPlatform(Calculation calculationLeft, Calculation calculationRight, int enemyCount, bool gateEnable)
    {
        if(gateEnable){
            platformEntryCalculation.setCalculationEntryPoints(calculationLeft, calculationRight);
            enemyGroupManager.setUpEnemies(true, enemyCount);
        }else{
            platformEntryCalculation.disableGate();
            enemyGroupManager.setUpEnemies(false, enemyCount);
        }
    }

    public Vector3 getLength(){
        return endPosition.position - starPosition.position;
    }

    public void DisableEnemies(){
        enemyGroupManager.DisableEnemies();
    }





}
