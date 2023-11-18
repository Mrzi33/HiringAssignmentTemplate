using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class to trigger player moving to enemy group, triggering check if player has enough health to continue
public class PlatformEnemyGate : MonoBehaviour
{
    [SerializeField]
    Platform platform;
    private void OnTriggerEnter(Collider other) {
        this.gameObject.SetActive(false);
        PlatformManager.Instance.OnEnemyGroupEnter();
    }
}
