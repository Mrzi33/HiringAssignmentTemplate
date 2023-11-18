using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class triggering end of the game if player reaches this platform
public class PlatformFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        GameManager.Instance.GameFinish();
    }
}
