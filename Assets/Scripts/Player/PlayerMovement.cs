using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//moves the player forward and sideways, movement is disabled on game over or finish
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float forwardSpeed = 5f;
    [SerializeField]
    float sideSpeed = 5f;

    [SerializeField]
    float leftSideLimit, rightSideLimit;

    public bool canMove = false;
    Vector3 inicialPosition = Vector3.zero;

    private void Awake() {
        inicialPosition = transform.position;
    }
    private void Start() {
        GameManager.Instance.OnGameStart.AddListener(OnGameStart);
        GameManager.Instance.OnGameOver.AddListener(OnGameOver);
        GameManager.Instance.OnGameFinish.AddListener(OnGameFinish);
    }

    

    private void OnGameStart()
    {
        transform.position = inicialPosition;
        canMove = true;
    }

    private void OnGameOver()
    {
        StartCoroutine(DisableMoveCoroutine());
    }
    private void OnGameFinish()
    {
        StartCoroutine(DisableMoveCoroutine());
    }

    IEnumerator DisableMoveCoroutine(){
        yield return new WaitForSeconds(1f);
        canMove = false;
    }



    private void Update()
    {
        if(canMove){
            Move();
        }

    }

    private void Move()
    {
        Vector3 movement = Vector3.zero;
        movement.x += forwardSpeed;
        if (Input.GetKey(KeyCode.A))
        {
            movement += new Vector3(0, 0, 5) * sideSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector3(0, 0, -5) * sideSpeed;
        }

        transform.position += movement * Time.deltaTime;
        transform.position = new Vector3(transform.position.x,transform.position.y, Mathf.Clamp(transform.position.z, leftSideLimit, rightSideLimit));
    }
    


}
