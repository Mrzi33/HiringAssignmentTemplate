using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnemyCalculation : MonoBehaviour
{
    [SerializeField]
    Platform platform;
    private void OnTriggerEnter(Collider other) {
        platform.EnemyGateEntered();
    }
}
