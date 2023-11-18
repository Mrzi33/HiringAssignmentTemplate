using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupManager : MonoBehaviour
{

    List<GameObject> enemies = new List<GameObject>();
    
    int enemyCount = 0;
    [SerializeField]
    BoxCollider spawnArea;
    [SerializeField]
    GameObject enemyGroup;
    [SerializeField]
    PlatformEnemyCalculation platformEnemyCalculation;



    public void setUpEnemies(bool enable, int enemyCount){
        this.enemyCount = enemyCount;
        if(enable){
            platformEnemyCalculation.gameObject.SetActive(true);
            SpawnEnemies();
        }else{
            platformEnemyCalculation.gameObject.SetActive(false);
        }

    }

    private void SpawnEnemies(){

        for(int i = 0; i < enemyCount; i++){
            Vector3 spawnPosition = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), 0.5f, Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
            GameObject enemy = EnemyCharacterManager.Instance.GetEnemyCharacter();
            enemy.transform.position = spawnPosition;
            enemy.transform.parent = enemyGroup.transform;
            enemies.Add(enemy);
        }

    }

    public void DisableEnemies(){
        foreach (var enemy in enemies)
        {
            EnemyCharacterManager.Instance.ReturnEnemyCharacter(enemy);
        }
        enemies.Clear();
    }


}
