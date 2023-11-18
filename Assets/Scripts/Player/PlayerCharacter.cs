using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerCharacter : MonoBehaviour
{
    Rigidbody rb;
    Transform groupCenter;
    [SerializeField]
    float movementSpeed = 1000;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        groupCenter = transform.parent;
    }

    private void FixedUpdate() {
        Vector3 centerVector = (groupCenter.position - transform.position);
        Vector3 direction = centerVector.normalized;
        float distance = centerVector.magnitude;

        rb.AddForce(direction * movementSpeed * distance * Time.fixedDeltaTime);
        //rb.position = GetComponentInParent<Transform>().position;
    }
    
}
