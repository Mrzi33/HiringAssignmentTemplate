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
    [SerializeField]
    EnemyGroupManager enemyGroupManager;
    [SerializeField]
    int enemyCount = 0;

    private void Awake() {
        platformEntryCalculation = GetComponentInChildren<PlatformEntryCalculation>();
        if(enableGate){
            platformEntryCalculation.setCalculationEntryPoints(leftCalculation, rightCalculation);
            enemyGroupManager.setUpEnemies(true, enemyCount);
        }else{
            platformEntryCalculation.disableCalculation();
            enemyGroupManager.setUpEnemies(false, enemyCount);
        }
    }


    public void EnemyGateEntered(){
        PlayerGroupManager.Instance.EnemyGateEntered(enemyCount);
    }


}
