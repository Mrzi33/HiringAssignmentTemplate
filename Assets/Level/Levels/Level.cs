using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level/Level")]

public class Level : ScriptableObject
{
    public List<PlatformSingle> platforms;
    public int emptyPlatformCount;
}
