using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class PlatformSingle{
    public Calculation calculationLeft;
    public Calculation calculationRight;
    public int enemyCount;
    public bool gateEnable;
}

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    GameObject platformPrefab;
    [SerializeField]
    List<PlatformSingle> individualPlatforms;
    [SerializeField]
    int emptyPlatformCount = 1;

    [SerializeField]
    Vector3 firstPlatformPosition;


    private void Awake() {
        Vector3 platformPosition = firstPlatformPosition;
        Vector3 platformLength = platformPrefab.GetComponent<Platform>().getLength();

        for (int i = 0; i < emptyPlatformCount; i++)
        {
            GameObject newPlatform = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            newPlatform.GetComponent<Platform>().SetPlatform(null, null, 0, false);
            newPlatform.transform.parent = this.transform;
            platformPosition += platformLength;
        }
        foreach (PlatformSingle platform in individualPlatforms)
        {
            GameObject newPlatform = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            newPlatform.GetComponent<Platform>().SetPlatform(platform.calculationLeft, platform.calculationRight, platform.enemyCount, platform.gateEnable);
            newPlatform.transform.parent = this.transform;
            platformPosition += platformLength;
        }

    }
}
