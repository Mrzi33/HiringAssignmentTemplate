using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to store the data of a single platform
[System.Serializable]
public class PlatformSingle{
    public Calculation calculationLeft;
    public Calculation calculationRight;
    public int enemyCount;
    public bool gateEnable;
}