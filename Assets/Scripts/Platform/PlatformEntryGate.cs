using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEntryGate : MonoBehaviour
{
    [SerializeField]
    bool leftGate;
    PlatformEntryCalculation platformEntryCalculation;
    [SerializeField]
    TMPro.TMP_Text textMesh;
    private void Awake() {
        platformEntryCalculation = GetComponentInParent<PlatformEntryCalculation>();
    }

    public void SetCalculation(Calculation calculation){
        textMesh.text = calculation.description;
    }


    private void OnTriggerEnter(Collider other) {
        platformEntryCalculation.gateEntered(leftGate);
    }



}
