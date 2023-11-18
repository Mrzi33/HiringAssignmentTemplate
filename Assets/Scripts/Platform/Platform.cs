using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    bool enableGate = true;
    [SerializeField]
    Calculation leftCalculation, rightCalculation;
    [SerializeField]

    PlatformEntryCalculation platformEntryCalculation;
    [SerializeField]
    EnemyGroupManager enemyGroupManager;
    [SerializeField]
    int enemyCount = 0;
    [SerializeField]
    Transform starPosition, endPosition;



    public void SetPlatform(Calculation calculationLeft, Calculation calculationRight, int enemyCount, bool gateEnable)
    {
        this.leftCalculation = calculationLeft;
        this.rightCalculation = calculationRight;
        this.enemyCount = enemyCount;
        this.enableGate = gateEnable;
        if(enableGate){
            platformEntryCalculation.setCalculationEntryPoints(leftCalculation, rightCalculation);
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
        Debug.Log("Disable Enemies");
        enemyGroupManager.DisableEnemies();
    }




}
