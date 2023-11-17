using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float forwardSpeed = 5f;
    [SerializeField]
    float sideSpeed = 5f;

    [SerializeField]
    float leftSideLimit, rightSideLimit;


    private void Update()
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
