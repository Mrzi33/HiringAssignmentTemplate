using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroupManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerSideCharacterPrefab;
    [SerializeField]
    List<GameObject> playerSideCharacters;
    [SerializeField]
    GameObject sideCharacterGroup;
    [SerializeField]
    List<Transform> playerSideCharacterPositions;
    [SerializeField]
    Transform mainCharacter;

    int score = 1;


    //make this classs a singleton
    private static PlayerGroupManager _instance;
    public static PlayerGroupManager Instance { get { return _instance; } }

    private void Awake() {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }
    }


    public void GateEntered(Calculation calculation){
        score = calculation.ApplyCaluclation(score);
        ManageGroup();
    }

    private void ManageGroup(){
        
        int groupCount = playerSideCharacters.Count;

        //enable side characters based on score, disable the rest
        for (int i = 0; i < groupCount; i++)
        {
            if(i < score-1){
                playerSideCharacters[i].SetActive(true);
            }else{
                playerSideCharacters[i].SetActive(false);
            }
        }
        //spawn new side characters if needed
        if(groupCount < score-1){
            for (int i = groupCount; i < score; i++)
            {
                SpawnSideCharacter();
            }
        }

        Debug.Log(score);

    }

    public void SpawnSideCharacter(){
        GameObject newSideCharacter = Instantiate(playerSideCharacterPrefab, sideCharacterGroup.transform);
        newSideCharacter.transform.position = playerSideCharacterPositions[playerSideCharacters.Count].position;
        playerSideCharacters.Add(newSideCharacter);
        newSideCharacter.GetComponent<SpringJoint>().connectedBody = mainCharacter.GetComponent<Rigidbody>();
    }

    internal void EnemyGateEntered(int enemyCount)
    {
        if(enemyCount > score){
            Debug.Log("Game Over");
        }else{
            score -= enemyCount;
            ManageGroup();
        }
    }
}
