using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void OnOverlayEnd(){
        GameManager.Instance.GameRestart();
    }
}
