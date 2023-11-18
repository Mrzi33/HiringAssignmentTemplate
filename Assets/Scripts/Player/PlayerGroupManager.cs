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

    public int score = 1;

    //create signal when score is 0 or less
    public event Action<bool> OnScoreChanged;




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
            if(i < score){
                playerSideCharacters[i].SetActive(true);
            }else{
                playerSideCharacters[i].SetActive(false);
            }
        }
        //spawn new side characters if needed
        if(groupCount < score){
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

    public bool EnemyGateEntered(int enemyCount)
    {
        score -= enemyCount;
        ManageGroup();
        if(score <= 0){
            OnScoreChanged?.Invoke(false);
            Debug.Log("Game Over");
            return false;

        }else{
            OnScoreChanged?.Invoke(true);
            return true;
        }
    }
    
}
