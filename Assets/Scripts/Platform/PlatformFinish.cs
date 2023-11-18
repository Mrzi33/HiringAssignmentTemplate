using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        GameManager.Instance.GameFinish();
    }
}
