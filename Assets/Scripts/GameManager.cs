using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;
    public UnityEvent OnGameRestart;

    public static GameManager Instance;

    private void Awake() {
        if(Instance != null && Instance != this){
            Destroy(this.gameObject);
        }else{
            Instance = this;
        }
    }

    public void GameStart(){
        OnGameStart.Invoke();
        Debug.Log("Game Started");
    }

    public void GameRestart(){
        OnGameRestart.Invoke();
    }

    public void GameOver(){
        OnGameOver.Invoke();
    }

    
}
