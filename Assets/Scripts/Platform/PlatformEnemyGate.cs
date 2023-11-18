using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnemyGate : MonoBehaviour
{
    [SerializeField]
    Platform platform;
    private void OnTriggerEnter(Collider other) {
        this.gameObject.SetActive(false);
        PlatformManager.Instance.OnEnemyGroupEnter();
    }
}
