using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for animation of finish overlay
public class FinishOverlay : MonoBehaviour
{
    Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        GameManager.Instance.OnGameFinish.AddListener(ShowFinish);
        
    }


    public void ShowFinish(){
        animator.SetTrigger("Show");
    }
    
    //called on animation end
    public void OnOverlayEnd(){
        GameManager.Instance.GameRestart();
    }
}
