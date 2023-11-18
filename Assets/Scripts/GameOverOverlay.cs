using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverOverlay : MonoBehaviour
{
    
    Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        GameManager.Instance.OnGameOver.AddListener(ShowGameOver);
        
    }


    public void ShowGameOver(){
        animator.SetTrigger("Show");
    }

    public void OnOverlayEnd(){
        GameManager.Instance.GameRestart();
    }


}
