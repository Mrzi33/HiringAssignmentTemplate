using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for animation of game over overlay
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

    //called on animation end
    public void OnOverlayEnd(){
        Debug.Log("Game Over");
        GameManager.Instance.GameRestart();
    }


}
