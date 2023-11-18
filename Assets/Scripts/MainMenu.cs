using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{


    private void Start() {
        GameManager.Instance.OnGameRestart.AddListener(OnGameRestart);
    }

    private void OnGameRestart()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void StartGame(int levelIndex){
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        GameManager.Instance.GameStart(levelIndex);
    }




}
