using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;
    public UnityEvent OnGameRestart;
    public UnityEvent OnGameFinish;

    public static GameManager Instance;
    [SerializeField]
    List<Level> levels;
    int currentLevel;

    private void Awake() {
        if(Instance != null && Instance != this){
            Destroy(this.gameObject);
        }else{
            Instance = this;
        }
    }

    public void GameStart(int level){
        currentLevel = level;
        OnGameStart.Invoke();
        Debug.Log("Game Started");
    }

    public void GameRestart(){
        OnGameRestart.Invoke();
    }

    public void GameOver(){
        OnGameOver.Invoke();
    }

    public void GameFinish(){
        OnGameFinish.Invoke();
    }

    public Level GetCurrentLevel()
    {
        return levels[currentLevel];
    }
}
