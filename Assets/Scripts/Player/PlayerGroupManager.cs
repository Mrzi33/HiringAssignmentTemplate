using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGroupManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerSideCharacterPrefab;
    [SerializeField]
    List<GameObject> playerSideCharacters;
    [SerializeField]
    GameObject sideCharacterGroup;

    [SerializeField]
    Transform mainCharacter;

    [SerializeField]
    BoxCollider spawnArea;
    public int health = 1;

    //create signal when score is 0 or less
    public UnityEvent OnHealthChanged;




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

    private void Start() {
        GameManager.Instance.OnGameStart.AddListener(OnGameStart);
    }

    private void OnGameStart()
    {
        health = 1;
        ManageGroup();
    }

    public void GateEntered(Calculation calculation){
        health = calculation.ApplyCaluclation(health);
        ManageGroup();
    }

    private void ManageGroup(){
        
        int groupCount = playerSideCharacters.Count;

        //enable side characters based on score, disable the rest
        for (int i = 0; i < groupCount; i++)
        {
            if(i < health){
                playerSideCharacters[i].SetActive(true);
            }else{
                playerSideCharacters[i].SetActive(false);
            }
        }
        //spawn new side characters if needed
        if(groupCount < health){
            for (int i = groupCount; i < health; i++)
            {
                SpawnSideCharacter();
            }
        }

        Debug.Log(health);

    }

    public void SpawnSideCharacter(){
        Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), 0.5f, UnityEngine.Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
        GameObject newSideCharacter = Instantiate(playerSideCharacterPrefab, sideCharacterGroup.transform);
        newSideCharacter.transform.position = spawnPosition;
        playerSideCharacters.Add(newSideCharacter);
        newSideCharacter.GetComponent<SpringJoint>().connectedBody = mainCharacter.GetComponent<Rigidbody>();
    }

    public bool EnemyGateEntered(int enemyCount)
    {
        //health -= enemyCount;
        //ManageGroup();
        if(health <= enemyCount){
            GameManager.Instance.GameOver();
            return false;

        }else{
            OnHealthChanged?.Invoke();
            return true;
        }
    }



    
}
