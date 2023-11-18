using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterManager : MonoBehaviour
{

    public static EnemyCharacterManager Instance;

    [SerializeField]
    GameObject enemyCharacterPrefab;
    Stack<GameObject> inactiveEnemyCharacters;

    private void Awake() {
        if(Instance != null && Instance != this){
            Destroy(this.gameObject);}
        else{
            Instance = this;
        }
        inactiveEnemyCharacters = new Stack<GameObject>();
    }

    public GameObject GetEnemyCharacter(){
        if(inactiveEnemyCharacters.Count == 0){
            GameObject newCharacter = Instantiate(enemyCharacterPrefab);
            return newCharacter;
        }else{
            GameObject character = inactiveEnemyCharacters.Pop();
            character.SetActive(true);
            return character;
        }
    }

    public void ReturnEnemyCharacter(GameObject character){
        character.SetActive(false);
        inactiveEnemyCharacters.Push(character);
        character.transform.parent = this.transform;
    }


}
