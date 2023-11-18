using System;
using System.Collections;
using System.Collections.Generic;
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



    public void EnemyGateEntered(){
        PlayerGroupManager.Instance.EnemyGateEntered(enemyCount);
    }

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
            platformEntryCalculation.disableCalculation();
            enemyGroupManager.setUpEnemies(false, enemyCount);
        }
    }

    public Vector3 getLength(){
        return endPosition.position - starPosition.position;
    }

}
