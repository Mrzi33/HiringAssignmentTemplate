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
        this.gameObject.SetActive(true);
    }

    public void StartGame(){
        this.gameObject.SetActive(false);
        GameManager.Instance.GameStart();
    }


}
