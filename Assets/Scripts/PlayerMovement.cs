using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    private void Update()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            movement += new Vector3(0, 0, 5);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector3(0, 0, -5);
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement += new Vector3(5, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += new Vector3(-5, 0, 0);
        }
        
        transform.position += movement * Time.deltaTime;
        transform.position = new Vector3(transform.position.x,transform.position.y, Mathf.Clamp(transform.position.z, -4.5f, 4.5f));

    }

}
