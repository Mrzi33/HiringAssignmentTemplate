using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class to manage enemy groups on individual platforms
public class EnemyGroupManager : MonoBehaviour
{

    [SerializeField]
    BoxCollider spawnArea;
    [SerializeField]
    GameObject enemyGroup;
    [SerializeField]
    PlatformEnemyGate platformEnemyGate;

    List<GameObject> enemies = new List<GameObject>();
    
    int enemyCount = 0;

    //plaotform sets up the enemy group, if the gate is enabled, it spawns enemies
    public void setUpEnemies(bool enable, int enemyCount){
        this.enemyCount = enemyCount;
        if(enable){
            platformEnemyGate.gameObject.SetActive(true);
            SpawnEnemies();
        }else{
            platformEnemyGate.gameObject.SetActive(false);
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
